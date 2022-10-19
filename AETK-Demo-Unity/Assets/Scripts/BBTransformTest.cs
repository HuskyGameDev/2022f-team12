using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamRotten;

public class BBTransformTest : MonoBehaviour
{
    public BoxCollider FromBox;
    public BoxCollider ToBox;
    public Transform FromPoint;

    private void OnDrawGizmos()
    {
        //var scale = AssMath.Vector3_Divide(ToBox.transform.lossyScale, FromBox.transform.lossyScale);
        
        
        Gizmos.DrawWireCube(FromBox.bounds.center, FromBox.bounds.size);
        Gizmos.DrawWireCube(ToBox.bounds.center, ToBox.bounds.size);
        Gizmos.DrawSphere(FromPoint.position, 0.2f);
        var destPoint = AssMath.BoxTransform(FromPoint.position, FromBox, ToBox);
        Gizmos.DrawSphere(destPoint, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
