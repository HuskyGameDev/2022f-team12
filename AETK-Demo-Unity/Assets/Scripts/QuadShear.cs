using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class QuadShear : MonoBehaviour
{
    public float TopShear = 0;
    public float BottomShear = 0;

    private Mesh mesh;
    private Vector3[] vertsOrigin;

    private void Awake()
    {
        // Playing
        if (Application.isPlaying)
        {
            this.mesh = GetComponent<MeshFilter>().mesh;
        }
        // Editor
        else
        {
            this.mesh = GetComponent<MeshFilter>().sharedMesh;
        }
        
        this.vertsOrigin = mesh.vertices;
    }

    private void Update()
    {
        Vector3[] verts = (Vector3[])vertsOrigin.Clone();

        verts[0].x += BottomShear;
        verts[1].x += BottomShear;
        verts[2].x += TopShear;
        verts[3].x += TopShear;

        mesh.vertices = verts;
    }
}
