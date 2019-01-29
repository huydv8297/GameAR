using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlane : MonoBehaviour {

	// Use this for initialization
    public int rows, columns;
    public float size = 1f;
    public Color color = Color.grey;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRenderObject()
    {
        //CreateLineMaterial();
        //lineMaterial.SetPass(0);

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        GL.Color(color);
        /* Horizontal lines. */
        for (int i = -rows / 2; i <= rows / 2; i++)
        {
            GL.Vertex3(-columns / 2 * size + transform.position.x, transform.position.y, transform.position.z + i* size);
            GL.Vertex3(columns / 2 * size + transform.position.x, transform.position.y, transform.position.z + i * size);
        }
        /* Vertical lines. */
        for (int i = -columns / 2; i <= columns / 2; i++)
        {
            GL.Vertex3(i * size + transform.position.x, transform.position.y, transform.position.z -rows/2 * size);
            GL.Vertex3(i * size + transform.position.x, transform.position.y, transform.position.z + rows/2 * size);
        }
        GL.End();
        GL.PopMatrix();
    }

}
