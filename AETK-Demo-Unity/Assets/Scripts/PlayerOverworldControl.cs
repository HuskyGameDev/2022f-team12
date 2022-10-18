using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PlayerOverworldControl : MonoBehaviour
{
    public enum GroundType { Flat, Slope };

    public float MoveSpeed = 0.5f;
    public float GravityAcceleration = 9.8f;
    public float JumpVel = 3f;
    public float MaxYVel = 1f;
    public float SlopeGroundingVel = -4f;
    //public float MaxSlopeCheckExtent = 4f;
    public Transform SpriteRoot;
    public CharacterController CController;

    private float yVel = 0f;
    private Vector3 collisionNormal;
    private GroundType groundType;

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( Input.GetButtonDown("IMenu_Toggle") )
        {
            Debug.Log("fdsaafds");
        }

        // If yVel was grounded upon last update, reset the yVel.
        if (this.CController.isGrounded)
        {
            // Set the initial yVel (at the start of this update) depending on the current ground. //
            switch (this.groundType)
            {
                case GroundType.Slope:
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

        // Set xMult to be the minimum of the speed multipliers determined by both casts.
        float xMult = this.CController.isGrounded ? Vector3.Dot(Vector3.up, this.collisionNormal) : 1f; //Mathf.Min(tL_Mult, tR_Mult);

        var xVel = Input.GetAxisRaw("Move_Horizontal") * MoveSpeed * xMult * Time.fixedDeltaTime;
        /*rbPos.x += xVel;*/

        // Limit yVel. //
        bool yVelNeg = yVel < 0;
        yVel = Mathf.Min(Mathf.Abs(yVel), MaxYVel);
        yVel *= yVelNeg ? -1 : 1;

        // Apply Position. //
        this.CController.Move(new Vector3(xVel, yVel));

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
                    this.groundType = GroundType.Slope;
                    break;
                default:
                    this.groundType = GroundType.Flat;
                    break;
            }
        }
        
        // print the impact point's normal
        //Debug.Log("Normal vector we collided at: " + hit.normal + ", CFlags: " + this.CController.collisionFlags);
    }
}
