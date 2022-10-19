using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransitionPoint : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "MoveTransitionDestination.png");
    }
}
