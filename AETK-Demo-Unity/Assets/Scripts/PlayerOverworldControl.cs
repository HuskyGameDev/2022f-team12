﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using MyBox;
using System.Linq;

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

    private float yVel = 0f;
    private Vector3 collisionNormal;
    private GroundTypes groundType;

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("IMenu_Toggle"))
        {
            Debug.Log("IMenu_Toggle Pressed");
        }

        if (Input.GetButtonDown("Overworld_Interact"))
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

        // If yVel was grounded upon last update, reset the yVel.
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
        }

        yVel -= GravityAcceleration * Time.fixedDeltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump!");

            yVel = JumpVel;
        }

        float xMult = this.CController.isGrounded ? Vector3.Dot(Vector3.up, this.collisionNormal) : 1f;
        float zMult = this.CController.isGrounded ? Vector3.Dot(Vector3.up, this.collisionNormal) : 1f;

        // Calculate xVel. //
        var xVel = Input.GetAxisRaw("Move_Horizontal") * MoveSpeedX * xMult * Time.fixedDeltaTime;

        // Calculate zVel depending on MovementType. //
        var zVel = 0f;

        if ( MovementType == MovementTypes.Depth )
        { 
            zVel = Input.GetAxisRaw("Move_Vertical") * MoveSpeedZ * zMult * Time.fixedDeltaTime;
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (this.CController.collisionFlags == CollisionFlags.Below)
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
}
