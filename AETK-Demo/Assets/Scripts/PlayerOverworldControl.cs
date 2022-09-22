using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverworldControl : MonoBehaviour
{
    public float MoveSpeed = 0.5f;
    public float GravityAcceleration = 9.8f;
    public float JumpVel = 3f;
    public float MaxYVel = 1f;
    public float MaxSlopeCheckExtent = 4f;
    public BoxCollider2D GroundCollider;

    private Rigidbody2D rb;
    private float yVel = 0f;
    private bool grounded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( Input.GetButtonDown("IMenu_Toggle") )
        {
            Debug.Log("fdsaafds");
        }
        
        // If not grounded apply gravity acceleration to yVel.
        if (!this.grounded)
        {
            yVel += GravityAcceleration * Time.fixedDeltaTime;
        }
        // If grounded, zero yVel;
        else 
        {
            yVel = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump!");

            yVel = -JumpVel;
            this.grounded = false;
        }

        // Store current rb pos. //
        var rbPos = rb.position;

        // Slope Speed correction. //
        var bounds = GroundCollider.bounds;
        var tL_Corner = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y + bounds.extents.y);
        var tR_Corner = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y + bounds.extents.y);

        var mask = ~LayerMask.GetMask("Player");
        var tL_Cast = Physics2D.Raycast(tL_Corner, Vector2.down, MaxSlopeCheckExtent, mask);
        var tR_Cast = Physics2D.Raycast(tR_Corner, Vector2.down, MaxSlopeCheckExtent, mask);

        Debug.DrawLine(tL_Corner, tL_Corner + Vector2.down * MaxSlopeCheckExtent);
        Debug.DrawLine(tR_Corner, tR_Corner + Vector2.down * MaxSlopeCheckExtent);

        float tL_Mult = 1f;
        if (tL_Cast.collider != null)
        {
            var norm = tL_Cast.normal;
            var angle = Mathf.Atan2(norm.y, norm.x);
            if (norm != Vector2.zero)
                tL_Mult = Mathf.Abs(Mathf.Sin(angle));
        }

        float tR_Mult = 1f;
        if (tR_Cast.collider != null)
        {
            var norm = tR_Cast.normal;
            var angle = Mathf.Atan2(norm.y, norm.x);
            if (norm != Vector2.zero)
                tR_Mult = Mathf.Abs(Mathf.Sin(angle));
        }

        // Set xMult to be the minimum of the speed multipliers determined by both casts.
        float xMult = Mathf.Min(tL_Mult, tR_Mult);

        rbPos.x += Input.GetAxisRaw("Move_Horizontal") * MoveSpeed * xMult * Time.fixedDeltaTime;

        // Limit yVel. //
        bool yVelNeg = yVel < 0;
        yVel = Mathf.Min(Mathf.Abs(yVel), MaxYVel);
        yVel *= yVelNeg ? -1 : 1;
        rbPos.y -= yVel;

        // Apply Position. //
        rb.position = rbPos;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
