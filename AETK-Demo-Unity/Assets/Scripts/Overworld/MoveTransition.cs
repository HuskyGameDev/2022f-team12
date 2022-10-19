using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using DG.Tweening;
using static PlayerOverworldControl;

public class MoveTransition : MonoBehaviour
{
    public enum TransitionTypes { ToDepth, ToFlat };
    public enum DestinationTypes { Area, Point };

    public TransitionTypes TransitionType;
    public DestinationTypes DestinationType;

    [ConditionalField(nameof(DestinationType), false, DestinationTypes.Point)]
    public Transform DestinationPoint;

    private void OnTriggerEnter(Collider collision)
    {
        var go = collision.gameObject;

        var oc = go.GetComponent<PlayerOverworldControl>();

        oc.enabled = false;

        TweenCallback onComplete = () => 
        {
            // Re-assign collision layer to Player object + all children. //
            int destLayer = 0;
            MovementTypes destMT = MovementTypes.Flat;

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

            go.layer = destLayer;

            foreach (Transform t in go.transform)
            {
                t.gameObject.layer = destLayer;
            }

            // Re-Enable Player Controller. //
            oc.enabled = true;
            oc.MovementType = destMT;
        };

        go.transform.DOMove(DestinationPoint.position, 2).OnComplete( onComplete );
    }

}
