using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class NavBuilder : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Build " + Time.realtimeSinceStartup.ToString());
            Debug.Log("Build finished " + Time.realtimeSinceStartup.ToString());
            navMeshSurface.BuildNavMesh();
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);

        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Update " + Time.realtimeSinceStartup.ToString());
        }
    }


}
