using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GoogleARCore;
using System;
using System.IO;
using UnityEngine.Networking;

public class RoadManager : MonoBehaviour, CursorEvent
{
    public Spline spline;
    public List<Spline> splines = new List<Spline>();
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
    public Toggle road, bridge, river, flag, car;
    public float heightRoad = 0f;
    public Text log;
    public static bool createRoadok=false;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    private bool m_IsQuitting = false;


    void Awake()
    {
        terrianController = GetComponent<TerrianController>();
    }

    public void OnClickDown()
    {
        Debug.Log("KeyDown");
        isCreate = true;
    }

    public void OnClickUp()
    {
        Debug.Log("KeyUp");

        if (spline != null)
        {
            Invoke("SetTag", 0.3f);
        }
        bridgeGO = null;
        isCreate = false;
    }

    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }


        Session.GetTrackables<DetectedPlane>(m_AllPlanes);

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            Debug.Log("KeyDown");
            //if (roadLine.isOn || roadHight.isOn)
            //    isRoadLine = false;
            isCreate = true;
        }

        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
        {
            Debug.Log("KeyUp");


            //if(river.isOn && spline != null)
            //{
            //    foreach (var node in spline.nodes)
            //    {
            //        terrianController.Dig(node.position);
            //    }
            //    spline.gameObject.SetActive(false);
            //}


            if (spline != null)
            {
                //if (roadHight.isOn || roadLine.isOn)
                //{

                //    isRoadLine = true;
                //    OnMouseDrag();
                //}

                Invoke("SetTag", 0.3f);
            }

            bridgeGO = null;
            isCreate = false;
        }

        if (isCreate&& createRoadok)
            OnMouseDrag();
    }

    public void ChangeMode(int mode)
    {
    }

    private void OnMouseDrag()
    {
        if (road.isOn)
            CreateRoad();
        //if (bridge.isOn)
        //    CreateBridge();
        //if (river.isOn)
        //    CreateRiver();
    }

    void SetTag()
    {

        foreach (GameObject child in spline.GetComponent<ExempleRailling>().meshes)
        {
            child.tag = "road";
        }

        spline = null;
    }

    void CreateRoad()
    {

        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.red, Mathf.Infinity);
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{

        //    if (hit.transform.CompareTag("wall"))
        //        return;
        if(CustomCursor.HitTransform(transform))
        { 

            heightRoad = Container.Instance.plane.transform.position.y + 0.01f;
            position = CustomCursor.currentHit.point;
            log.text = "" + position;
            position.y = heightRoad;

            if (spline == null)
            {
                GameObject newSpline = Instantiate(roadGeneretorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                NetworkServer.Spawn(newSpline);
                newSpline.transform.parent = Container.Instance.transform;
                spline = newSpline.GetComponent<Spline>();
                splines.Add(spline);
                spline.nodes[0].position = position;
                spline.nodes[1].position = position;

                Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.1f);
                spline.nodes[0].direction = firstDirection;
                spline.nodes[1].direction = firstDirection;
            }
            else
            {
                lastNode = spline.nodes.LastOrDefault();
                //lastNode.position.y = heightRoad;
                float distance = Vector3.Distance(lastNode.position, position);

                if (distance < space)
                    return;
                else
                {
                    float count = distance / space;
                    //Vector3 between = GetPoint(lastNode.position, position, 0.5f);
                    //int midpoint = (int)count / 2;
                    for (float i = 1; i < count; i++)
                    {
                        Vector3 _position = GetPoint(lastNode.position, position, i / count);
                        //if(roadHight.isOn)
                        //{
                        //    Vector3 _position2 = GetPoint(lastNode.position, position, i / count);

                        //}
                        AddNode(_position);
                        log.text = "AddNode" + i;

                    }
                }

                //}
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

                if (bridgeGO == null)
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

        Vector3 _direction = GetPoint(_lastNode.position, _postion, 1.2f);

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

    public void SaveRoad()
    {
        RoadData roadData = new RoadData();
        roadData.position = new Vector3[splines[0].nodes.Count];
        roadData.direction = new Vector3[splines[0].nodes.Count];
        for (int i = 0; i < spline.nodes.Count; i++ )
        {
            roadData.position[i] = spline.nodes[i].position;
            roadData.direction[i] = spline.nodes[i].direction;
        }
        string dataAsJson = JsonUtility.ToJson(roadData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
    public string gameDataProjectFilePath = "/Data/road.json";
    public RoadData roadData;

    public void LoadData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            roadData = JsonUtility.FromJson<RoadData>(dataAsJson);

            GameObject newSpline = Instantiate(roadGeneretorPrefab, Container.Instance.transform) as GameObject;
            newSpline.transform.parent = Container.Instance.transform;
            Spline spline = newSpline.GetComponent<Spline>();
            splines.Add(spline);
            SplineNode node0 = new SplineNode(roadData.position[0], roadData.direction[0]);
            spline.nodes[0] = node0;

            SplineNode node1 = new SplineNode(roadData.position[1], roadData.direction[1]);
            spline.nodes[1] = node1;

            for (int i = 2; i < roadData.position.Length; i++)
            {
                SplineNode node = new SplineNode(roadData.position[i], roadData.direction[i]);
                spline.AddNode(node);
            }

        }
        else
        {
        }

    }

    
}

[Serializable]
public class RoadData
{
    public Vector3[] position;
    public Vector3[] direction;
}


