using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterControllerInput : MonoBehaviour
{
    public CharacterController cc;
    public SpriteRenderer sr;

    public Vector2 MovementSpeed;
    public float Gravity = 9.8f;
    public float LerpPerc = 0.8f;

    private Vector2 lastControlVel;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vel;

        Vector2 rawControl = new Vector2(Input.GetAxisRaw("Move_Horizontal"), Input.GetAxisRaw("Move_Vertical"));
        rawControl.Normalize();

        lastControlVel = Vector2.Lerp(lastControlVel, rawControl, LerpPerc) ;


        if (lastControlVel.x > 0)
        {
            sr.flipX = false;
        }
        else if (lastControlVel.x < 0)
        {
            sr.flipX = true;
        }

        Vector2 moveVec = Vector2.Scale(lastControlVel, MovementSpeed) * Time.deltaTime;

        vel = new Vector3( moveVec.x, -Gravity, moveVec.y );

        cc.Move(vel);
    }
}
