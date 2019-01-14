using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	public List<GameObject> flags = new List<GameObject>();
    public GameObject prefab;
    public RoadManager roadManager;
    public List<SplineNode> nodes = new List<SplineNode>();
    public bool isMoveable;
    public GameObject flag;

    public void AddFlag()
    {
        flag = Instantiate(prefab, nodes[1].position, Quaternion.identity);
        flags.Add(flag);
    }

	void Start () {
        nodes = roadManager.spline.nodes;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isMoveable = true;
            
                

        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            isMoveable = false;
            if (flags.Count > 1)
            {
                Debug.DrawLine(flags[flags.IndexOf(flag) - 1].transform.position, flag.transform.position, Color.blue, Mathf.Infinity);
            }
        }

        if (isMoveable)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "terria")
                {
                    flag.transform.position = FindMinDistance(hit.point);
                }
            }

            if (flags.Count > 1)
            {
                Debug.DrawLine(flags[flags.IndexOf(flag) - 1].transform.position, flag.transform.position, Color.blue);
            }
        }
    }

    Vector3 FindMinDistance(Vector3 point)
    {
        float min = Vector3.Distance(point, nodes[0].position);
        Vector3 minNode = Vector3.zero;
        foreach(var node in nodes)
        {
            if (min > Vector3.Distance(point, node.position))
            {
                min = Vector3.Distance(point, node.position);
                minNode = node.position;
            }
        }
        return minNode;
    }

    void DrawLine()
    {
        for(int i = 1; i < nodes.Count; i++)
        {
            Debug.DrawLine(nodes[i-1].position, nodes[i].position);
        }
    }

}
