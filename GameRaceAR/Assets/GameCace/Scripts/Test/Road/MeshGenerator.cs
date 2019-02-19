using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    
    int[] tris = new int[6];
    // Use this for initialization
    void Start() {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        

        Vector3[] verts = new Vector3[]{
            new Vector3(0.0f, 0f, 0.0f),
            new Vector3(0.0f, 0f, 1.0f),
            new Vector3(1.0f, 0f, 0.0f),
            new Vector3(1.0f, 0f, 1.0f)
         };

        Vector2[] uvs = new Vector2[] {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, 1.0f)
        };
        

        tris[0] = 0;
        tris[1] = 1;
        tris[2] = 2;

        tris[3] = 0;
        tris[4] = 2;
        tris[5] = 3;

        //mesh.vertices = verts;
        //mesh.uv = uvs;
        //mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        
    }

    // Update is called once per frame
    void Update () {
        GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, transform.localScale.z);
    }
}
