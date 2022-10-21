using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Events;
using System;

/// <summary>
/// A script to be used as an intermediary between normal collisions and IInteractables.
/// </summary>
public class CollisionInteraction : MonoBehaviour
{
    public enum CollisionTypes { OnCollision, OnTrigger };

    public CollisionTypes CollisionType = CollisionTypes.OnTrigger;
    [Separator]
    public UnityEvent<PlayerOverworldControl> OnCollide;

    private void addOnGroundDelegate( PlayerOverworldControl oc )
    {
        // Create a temporary action which invokes OnCollide, and removes itself      //
        // from oc.OnGrounded, as to only run right when the player is next grounded. //
        Action og = null;
        og = () =>
        {
            OnCollide.Invoke(oc);

            // Remove self from event.
            oc.OnGrounded -= og;
        };

        oc.OnGrounded += og;
    }

    // Collision Functions. //

    private void OnCollisionEnter(Collision collision)
    {
        if ( CollisionType != CollisionTypes.OnCollision )
            return;

        // If the collided object is the player. //
        if (collision.gameObject.CompareTag("Player"))
        {
            var oc = collision.gameObject.GetComponent<PlayerOverworldControl>();

            // If the player is not grounded, add to the waitlist. //
            if (oc.IsGrounded())
                OnCollide.Invoke(oc);
            // Otherwise, add our OnGround delegate action to the PlayerOverworldControl. //
            else
                addOnGroundDelegate(oc);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CollisionType != CollisionTypes.OnTrigger)
            return;

        // If the collided object is the player. //
        if (other.gameObject.CompareTag("Player"))
        {
            var oc = other.gameObject.GetComponent<PlayerOverworldControl>();

            // If the player is not grounded, add to the waitlist. //
            if (oc.IsGrounded())
                OnCollide.Invoke(oc);
            // Otherwise, add our OnGround delegate action to the PlayerOverworldControl. //
            else
                addOnGroundDelegate(oc);
        }
    }
}
