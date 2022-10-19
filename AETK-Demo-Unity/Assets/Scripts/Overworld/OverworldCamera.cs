using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCamera : MonoBehaviour
{
    public enum LerpUpdate { Update, FixedUpdate };

    public Transform TrackingTarget;

    public Camera Camera;
    public float LerpSpeed = 0.5f;
    public LerpUpdate UpdateType = LerpUpdate.FixedUpdate;

    private void Update()
    {
        if (this.UpdateType == LerpUpdate.Update)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, TrackingTarget.position, LerpSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (this.UpdateType == LerpUpdate.FixedUpdate)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, TrackingTarget.position, LerpSpeed);
        }
    }
}
