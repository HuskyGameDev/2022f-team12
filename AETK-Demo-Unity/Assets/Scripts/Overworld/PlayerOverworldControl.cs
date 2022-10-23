using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using MyBox;
using System.Linq;
using System;

public class PlayerOverworldControl : MonoBehaviour
{
    public enum MovementTypes { Flat, Depth };
    public enum GroundTypes { Flat, Slope };

    public float MoveSpeedX = 0.5f;
    public float MoveSpeedZ = 0.5f;
    [Separator]
    public float GravityAcceleration = 9.8f;
    public float JumpVel = 3f;
    public float MaxYVel = 1f;
    public float SlopeGroundingVel = -4f;
    public Transform SpriteRoot;
    public CharacterController CController;
    [Separator]
    public Transform InteractOrigin;
    public float InteractRadius;

    public MovementTypes MovementType
    {
        get { return movementType; }
        set { movementType = value; }
    }

    [Separator]

    [SerializeField]
    private MovementTypes movementType = MovementTypes.Flat;

    public event Action OnGrounded = () => { };

    private float yVel = 0f;
    private Vector3 collisionNormal;
    private GroundTypes groundType;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("IMenu_Toggle"))
        {
            Debug.Log("IMenu_Toggle Pressed");
        }

        if ( !CController.isGrounded )
            goto PostInteractionCheck;

        // Check for overlapping Overworld interactions if the interact button is pressed. //
        if (Input.GetButtonDown("OW_Interact"))
        {
            string castLayer = "";

            switch ( movementType )
            {
                case MovementTypes.Flat:
                    castLayer = "Trig_FlatMove";
                    break;
                case MovementTypes.Depth:
                    castLayer = "Trig_DepthMove";
                    break;
            }

            var cast = 
                Physics.OverlapSphere(
                    InteractOrigin.position, 
                    InteractRadius, 
                    LayerMask.GetMask(castLayer), 
                    QueryTriggerInteraction.Collide
                    );

            // If there are multiple collisions, determine the closest //
            // collider whose GO contains an IInteractable.            //
            float lDist = float.MaxValue;
            IInteractable lInter = null;

            foreach (var c in cast)
            {
                var inter = c.GetComponent<IInteractable>();

                if (inter == null)
                    continue;

                var dist = Vector3.Distance(transform.position, c.transform.position);

                if ( dist < lDist )
                {
                    lDist = dist;
                    lInter = inter;
                }
            }

            if (lInter != null)
                lInter.OnInteract(this);
        }
        // Check for overlapping flat-doorway interactions if the doorway //
        // interact button is pressed and the player is in FlatMove.      //
        else if (
            Input.GetButtonDown("OW_Flat_Doorway_Interact") && 
            movementType == MovementTypes.Flat
            )
        {

            var cast =
                Physics.OverlapSphere(
                    InteractOrigin.position,
                    InteractRadius,
                    LayerMask.GetMask("Trig_FlatMove"),
                    QueryTriggerInteraction.Collide
                    );

            // If there are multiple collisions, determine the closest //
            // collider whose GO contains an IInteractable.            //
            float lDist = float.MaxValue;
            IFlatDoorway lInter = null;

            foreach (var c in cast)
            {
                var inter = c.GetComponent<IFlatDoorway>();

                if (inter == null)
                    continue;

                var dist = Vector3.Distance(transform.position, c.transform.position);

                if (dist < lDist)
                {
                    lDist = dist;
                    lInter = inter;
                }
            }

            if (lInter != null)
                lInter.OnInteract(this);
        }

        // Code flag for right after all of the interaction checks.
        PostInteractionCheck:

        // If yVel was grounded upon last update, reset the yVel. //
        // Also, Invoke OnGrounded.                               //
        if (this.CController.isGrounded)
        {
            // Set the initial yVel (at the start of this update) depending on the current ground. //
            switch (this.groundType)
            {
                case GroundTypes.Slope:
                    yVel = SlopeGroundingVel;
                    break;
                default:
                    yVel = 0;
                    break;
            }

            // Invoke OnGrounded.
            OnGrounded.Invoke();
        }

        yVel -= GravityAcceleration * Time.fixedDeltaTime;

        if (Input.GetButtonDown("OW_Jump"))
        {
            Debug.Log("Jump!");

            yVel = JumpVel;
        }

        float xMult = this.CController.isGrounded ? Vector3.Dot(Vector3.up, this.collisionNormal) : 1f;
        float zMult = this.CController.isGrounded ? Vector3.Dot(Vector3.up, this.collisionNormal) : 1f;

        // Calculate xVel. //
        var xVel = Input.GetAxisRaw("OW_Move_Horizontal") * MoveSpeedX * xMult * Time.fixedDeltaTime;

        // Calculate zVel depending on MovementType. //
        var zVel = 0f;

        if ( MovementType == MovementTypes.Depth )
        { 
            zVel = Input.GetAxisRaw("OW_Move_Vertical") * MoveSpeedZ * zMult * Time.fixedDeltaTime;
        }

        // Limit yVel. //
        bool yVelNeg = yVel < 0;
        yVel = Mathf.Min(Mathf.Abs(yVel), MaxYVel);
        yVel *= yVelNeg ? -1 : 1;

        // Apply Position. //
	    this.CController.Move(new Vector3(xVel, yVel, zVel));

        // Set Sprite Flip. //
        var scale = this.SpriteRoot.localScale;

        if (xVel > 0)
            scale.x = 1;
        else if (xVel < 0)
            scale.x = -1;

        this.SpriteRoot.localScale = scale;
    }

    public bool IsGrounded()
    {
        return CController.isGrounded;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("CCColliderHit - CF: " + this.CController.collisionFlags);

        // If we're reported as being grounded, evaluate what kind of surface that we're on. //
        // Note: CollisionFlags.None seems to be a weird edge-case result of running down    //
        //       a slope really fast. A collision occurs, but for some stupid reason         //
        //       collisionFlags reports it as "None".                                        //
        if ( 
            this.CController.collisionFlags == CollisionFlags.Below || 
            this.CController.collisionFlags == CollisionFlags.None )
        {
            this.collisionNormal = hit.normal;

            switch (hit.collider.tag)
            {
                case "Col_Slope":
                    this.groundType = GroundTypes.Slope;
                    break;
                default:
                    this.groundType = GroundTypes.Flat;
                    break;
            }
        }
        
        // print the impact point's normal
        //Debug.Log("Normal vector we collided at: " + hit.normal + ", CFlags: " + this.CController.collisionFlags);
    }

    private void OnDrawGizmos()
    {
        // Draw Interact Sphere.
        Gizmos.DrawWireSphere(InteractOrigin.position, InteractRadius);
    }

    private void OnGUI()
    {
        /*GUI.Box(Rect(10, 10, 120, 25), "Vel: " + CController.velocity);*/
        
    }
}
