using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Represents a point to serve at the destination for a player MoveTransition.
/// </summary>
[ExecuteInEditMode]
public class MoveTransitionPoint : MonoBehaviour
{
    public bool CastToGround = false;

    private int raycastMask = -1;


    private void Awake()
    { 
        raycastMask = LayerMask.GetMask("Col_FlatMove", "Col_DepthMove");
    }

    public Vector3 GetTransitionPoint()
    {
        var pos = transform.position;

        RaycastHit rh;
        var hit = Physics.Raycast(new Ray(pos, Vector3.down), out rh, Mathf.Infinity, raycastMask);

        if (hit)
            return rh.point;
        else
            return pos;
    }

    private void OnDrawGizmos()
    {
        var pos = transform.position;

        Gizmos.DrawIcon(pos, "MoveTransitionDestination.png");

        if (CastToGround)
        {
            var tp = GetTransitionPoint();

            Handles.color = Color.white;

            // Draw line down to ground. //
            Handles.DrawDottedLine(pos, tp, 4f);

            // Draw target on ground. //
            Handles.DrawWireDisc(tp, Vector3.up, 0.5f);
            Handles.DrawWireDisc(tp, Vector3.up, 0.2f);
        }
    }
}
