using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Follow;
    public bool FollowX = true;
    public bool FollowY = false;
    public bool FollowZ = false;
    public float LerpFollow = 1f;

    void FixedUpdate()
    {
        var pos = transform.position;
        var fpos = Follow.position;

        var newX = FollowX ? Mathf.Lerp(pos.x, fpos.x, LerpFollow) : pos.x;
        var newY = FollowY ? Mathf.Lerp(pos.y, fpos.y, LerpFollow) : pos.y;
        var newZ = FollowZ ? Mathf.Lerp(pos.z, fpos.z, LerpFollow) : pos.z;

        pos = new Vector3( newX, newY, newZ );

        transform.position = pos;
    }
}
