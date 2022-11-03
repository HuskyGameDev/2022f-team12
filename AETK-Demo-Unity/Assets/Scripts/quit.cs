using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Quit");
#if UNITY_EDITOR
   UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
