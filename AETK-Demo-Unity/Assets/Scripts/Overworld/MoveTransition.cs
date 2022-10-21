using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using DG.Tweening;
using static PlayerOverworldControl;
using TeamRotten;

public class MoveTransition : MonoBehaviour, IFlatDoorway
{
    public enum TransitionTypes { ToDepth, ToFlat };
    public enum DestinationTypes { Area, Point };

    public TransitionTypes TransitionType;
    public DestinationTypes DestinationType;

    public float TransitionTime = 1f;

    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Point)]
    public MoveTransitionPoint DestinationPoint;

    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Area)]
    public BoxCollider OriginBox;
    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Area)]
    public BoxCollider DestinationBox;

    public void OnInteract( PlayerOverworldControl pc )
    {
        var go = pc.gameObject;
        var go_tf = go.transform;

        // Disable the Player Controller.
        pc.enabled = false;

        // Determine the destination position + layer. //
        Vector3 destPos = go_tf.position;
        int destLayer = 0;
        MovementTypes destMT = MovementTypes.Flat;

        switch (DestinationType)
        {
            case DestinationTypes.Point:
                destPos = DestinationPoint.GetTransitionPoint();
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

        TweenCallback onComplete = () =>
        {
            // Re-assign collision layer to Player object + all children. //
            go.layer = destLayer;

            foreach (Transform t in go.transform)
            {
                t.gameObject.layer = destLayer;
            }

            // Re-Enable Player Controller. //
            pc.enabled = true;
            pc.MovementType = destMT;
        };

        // Perform the transition. //
        go.transform.DOMove(destPos, TransitionTime).OnComplete(onComplete);
    }
}
