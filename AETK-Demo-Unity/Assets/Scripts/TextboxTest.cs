using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxTest : MonoBehaviour
{
    public Vector3 TextScale = Vector3.one;

    public Dialoguer dialoguer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Pssst. Press Space.");
        dialoguer.ReadDialogue("DialogueTest");
    }

    private static void ScaleV3(ref Vector3 vert, Vector3 middle, Vector3 scale)
    {
        var oDist = vert - middle;

        vert = middle + Vector3.Scale(oDist, scale);
    }

    public void ChangeVertices(Vector3[] verts, Vector3[] middles, Vector3[] positions)
    {

        for (int i = 0; i < verts.Length / 4; i++)

        {

            var tl = verts[4 * i + 0];
            var tr = verts[4 * i + 1];
            var br = verts[4 * i + 2];
            var bl = verts[4 * i + 3];

            var realMiddle = Vector3.Lerp(tl, br, 0.5f);

            //change a letter's verts:

            ScaleV3(ref verts[4 * i + 0], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 1], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 2], realMiddle, TextScale);
            ScaleV3(ref verts[4 * i + 3], realMiddle, TextScale);

            /*verts[4 * i + 0] += Vector3.right;

            verts[4 * i + 1] += Vector3.right;

            verts[4 * i + 2] += Vector3.right;

            verts[4 * i + 3] += Vector3.right;*/

            //move all to the right
        }

    }
}
