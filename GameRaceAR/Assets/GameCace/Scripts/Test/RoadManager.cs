using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadManager : MonoBehaviour
{
    public Spline spline;
    public SplineNode lastNode;
    public bool isCreate = false, isRoadLine = true;
    public int f = 10;
    public Vector3 position;
    public float space = 30f, height = 3f;
    public Vector3 scale = Vector3.zero;
    public GameObject roadGeneretorPrefab;
    public GameObject bridgeGeneretorPrefab;
    public TerrianController terrianController;
    public GameObject bridgeGO;
    public Toggle road, bridge, river, roadLine, roadHight;


    void Awake()
    {
        terrianController = GetComponent<TerrianController>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Debug.Log("KeyDown");
            if (roadLine.isOn || roadHight.isOn)
                isRoadLine = false;
            isCreate = true;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
        {
            Debug.Log("KeyUp");
            

            if(river.isOn && spline != null)
            {
                foreach (var node in spline.nodes)
                {
                    terrianController.Dig(node.position);
                }
                spline.gameObject.SetActive(false);
            }

            
            if (spline != null)
            {
                if (roadHight.isOn || roadLine.isOn)
                {
                    
                    isRoadLine = true;
                    OnMouseDrag();
                }

                Invoke("SetTag", 0.5f);
                
            }
                
            bridgeGO = null;
            isCreate = false;
        }


        if (isCreate)
            OnMouseDrag();


    }

    public void ChangeMode(int mode)
    {
    }

    private void OnMouseDrag()
    {
        if(road.isOn || roadLine.isOn || roadHight.isOn)
            CreateRoad();
        if (bridge.isOn)
            CreateBridge();
        if (river.isOn)
            CreateRiver();
    }

    void SetTag()
    {
        Debug.Log("Tag");
        foreach(GameObject child in spline.GetComponent<ExempleRailling>().meshes)
        {
            Debug.Log(spline.GetComponent<ExempleRailling>().meshes.Count);
            child.tag = "terria";
        }

        spline = null;
    }

    void CreateRoad()
    {
        Debug.Log("OnDrag");
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "terria")
            {
                    position = hit.point;
                    if (spline == null)
                    {
                        GameObject newSpline = Instantiate(roadGeneretorPrefab, roadGeneretorPrefab.transform.position, Quaternion.identity) as GameObject;
                        newSpline.transform.parent = transform;
                        spline = newSpline.GetComponent<Spline>();
                        spline.nodes[0].position = position;
                        spline.nodes[1].position = position;
                        Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.5f);
                        spline.nodes[0].direction = firstDirection;
                        spline.nodes[1].direction = firstDirection;
                    }
                    else if(isRoadLine)
                    {
                        lastNode = spline.nodes.LastOrDefault();
                        float distance = Vector3.Distance(lastNode.position, position);
                        if (distance < space)
                            return;

                        float count = distance / space;
                        Vector3 between = GetPoint(lastNode.position, position, 0.5f);
                        int midpoint = (int)count / 2;
                        for (float i = 1; i < count; i++)
                        {
                            Vector3 _position = GetPoint(lastNode.position, position, i / count);
                            if(roadHight.isOn)
                            {
                                Vector3 _position2 = GetPoint(lastNode.position, position, i / count);
                                
                            }

                            AddNode(_position);
                            Debug.Log("AddNode");
                        }                    
                    }
                }
            }
    }

    void CreateBridge()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "terria")
            {
                Vector3 position = hit.point;
                position.y = bridgeGeneretorPrefab.transform.position.y;
                
                if(bridgeGO == null)
                {
                    
                    bridgeGO = Instantiate(bridgeGeneretorPrefab, bridgeGeneretorPrefab.transform.position, Quaternion.identity, transform) as GameObject;
                }
                else
                {
                    bridgeGO.transform.position = position;
                    Vector3 temp = Vector3.zero;
                    temp.y = Camera.main.transform.localRotation.y;
                    bridgeGO.transform.localRotation = Quaternion.Euler(temp);
                }
            }
        }
    }

    void CreateRiver()
    {
        CreateRoad();
    }

    Vector3 GetPoint(Vector3 start, Vector3 end, float percent)
    {
        return (start + percent * (end - start));
    }

    void AddNode(Vector3 position)
    {
        SplineNode _lastNode = spline.nodes.LastOrDefault();
        

        Vector3 _postion = position;
        ////if(_postion.y - spline.nodes[0].position.y > 2f)
        ////    _postion.y += height;
        //_postion.y = _lastNode.position.y;
        Vector3 _direction = _postion;

        _direction.y += (Mathf.Abs(_postion.y) - Mathf.Abs(_lastNode.position.y)) / f;
        _direction.z += (Mathf.Abs(_postion.z) - Mathf.Abs(_lastNode.position.z)) / f;
        _direction.x += (Mathf.Abs(_postion.x) - Mathf.Abs(_lastNode.position.x)) / f;
        SplineNode _node = new SplineNode(_postion, _direction);
        spline.AddNode(_node);
        if (spline.nodes[0].position == spline.nodes[1].position)
        {
            spline.nodes[0].direction = GetPoint(spline.nodes[0].position, spline.nodes[2].position, 0.5f);
            spline.RemoveNode(spline.nodes[1]);
        }

      
            //if (road.isOn)
            //    BendRoad(_node);
        }

    void BendRoad(SplineNode node)
    {
        int index = spline.nodes.IndexOf(node);
        if (index <= 2)
            return;
        SplineNode _lastNode = spline.nodes[index - 1];
        BendRoad(_lastNode);
        _lastNode = spline.nodes[index - 1];
        Vector3 part = _lastNode.position - _lastNode.direction;
        Vector3 furure = node.position - _lastNode.position;
        if (Vector3.Angle(part, furure) < 90)
        {
            spline.RemoveNode(_lastNode);
        }
    }
}
