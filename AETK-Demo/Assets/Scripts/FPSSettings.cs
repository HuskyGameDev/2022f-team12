using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSettings
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("ONLOAD");

        // If running on a high refresh display, disable v-sync
        // (Otherwise, Unity will *not* cap the framerate as asked).
        if (Screen.currentResolution.refreshRate == 60)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
        }

        // Limit max queued frames to one to cut back on render-to-display latency.
        QualitySettings.maxQueuedFrames = 1;
    }
}
