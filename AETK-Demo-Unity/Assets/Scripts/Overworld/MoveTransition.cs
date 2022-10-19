using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using DG.Tweening;
using static PlayerOverworldControl;
using TeamRotten;

public class MoveTransition : MonoBehaviour
{
    public enum TransitionTypes { ToDepth, ToFlat };
    public enum DestinationTypes { Area, Point };

    public TransitionTypes TransitionType;
    public DestinationTypes DestinationType;

    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Point)]
    public Transform DestinationPoint;

    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Area)]
    public BoxCollider OriginBox;
    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Area)]
    public BoxCollider DestinationBox;

    private Vector3? destPosTest = null;
    private void OnTriggerEnter(Collider collision)
    {
        var go = collision.gameObject;
        var go_tf = go.transform;

        var oc = go.GetComponent<PlayerOverworldControl>();

        oc.enabled = false;

        Vector3 destPos = go_tf.position;
        int destLayer = 0;
        MovementTypes destMT = MovementTypes.Flat;

        switch (DestinationType)
        {
            case DestinationTypes.Point:
                destPos = DestinationPoint.position;
                break;
            case DestinationTypes.Area:
                destPos = AssMath.BoxTransform(go_tf.position, OriginBox, DestinationBox);
                break;
        }

        switch (TransitionType)
        {
            case TransitionTypes.ToDepth:
                destLayer = LayerMask.NameToLayer("Col_DepthMove");
                destMT = MovementTypes.Depth;

                break;
            case TransitionTypes.ToFlat:
                destLayer = LayerMask.NameToLayer("Col_FlatMove");
                destMT = MovementTypes.Flat;
                break;
        }

        this.destPosTest = destPos;

        TweenCallback onComplete = () => 
        {
            // Re-assign collision layer to Player object + all children. //
            go.layer = destLayer;

            foreach (Transform t in go.transform)
            {
                t.gameObject.layer = destLayer;
            }

            // Re-Enable Player Controller. //
            oc.enabled = true;
            oc.MovementType = destMT;
        };

        go.transform.DOMove(destPos, 2).OnComplete( onComplete );
    }

    private void OnDrawGizmos()
    {
        if (this.destPosTest.HasValue)
        {
            Gizmos.DrawSphere(this.destPosTest.Value, 1f);
        }
    }

}
