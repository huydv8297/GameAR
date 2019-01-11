using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathTest : MonoBehaviour {
    List<Transform> path;
    public Color rayColor = Color.white;
    

    // Use this for initialization
    void Start () {
   
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Transform[] path_objs;
        path_objs = transform.GetComponentsInChildren<Transform>();
        path = new List<Transform>();
        

        foreach(Transform path_obj in path_objs)
        {
            if(path_obj != transform)
            {
                path.Add(path_obj);
            }
        }
        for(int i = 0; i< path.Count; i++)
        {
            Vector3 pos = path[i].position;
            
            if (i > 0)
            {
                Vector3 prev = path[i - 1].position;
                Gizmos.DrawLine(prev, pos);
                Gizmos.DrawWireSphere(pos, 0.1f);
            }
        }
    }
}
