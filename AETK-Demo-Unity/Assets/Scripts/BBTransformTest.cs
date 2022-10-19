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
        var scale = AssMath.Vector3_Divide(Vector3.Scale(ToBox.size, ToBox.transform.lossyScale), Vector3.Scale(FromBox.size, FromBox.transform.lossyScale));

        Debug.Log("ScaleVal: " + scale);
        // Create a translation matrix between the two Colliders.
        var trs = Matrix4x4.TRS
        (
            ToBox.bounds.center - FromBox.bounds.center,
            ToBox.transform.rotation * Quaternion.Inverse(FromBox.transform.rotation),
            scale
        );
        
        Gizmos.DrawWireCube(FromBox.bounds.center, FromBox.bounds.size);
        Gizmos.DrawWireCube(ToBox.bounds.center, ToBox.bounds.size);
        Gizmos.DrawSphere(FromPoint.position, 0.2f);
        Gizmos.DrawSphere(trs.MultiplyPoint(FromPoint.transform.position), 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
