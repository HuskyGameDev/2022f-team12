using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    public Collider PlatformCollider;
    public Collider TriggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(PlatformCollider, other, true);
    }
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(PlatformCollider, other, false);
    }
}
