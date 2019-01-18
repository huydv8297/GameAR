using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GoogleARCore;
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
    public float heightRoad = 0f;
    public Text log;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    private bool m_IsQuitting = false;


    void Awake()
    {
        terrianController = GetComponent<TerrianController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // Hide snackbar when currently tracking at least one plane.
        // 
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

            
            //if (spline != null)
            //{
            //    if (roadHight.isOn || roadLine.isOn)
            //    {
                    
            //        isRoadLine = true;
            //        OnMouseDrag();
            //    }

            //    Invoke("SetTag", 0.5f);
                
            //}

            spline = null;
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
        //if (bridge.isOn)
        //    CreateBridge();
        //if (river.isOn)
        //    CreateRiver();
    }

    void SetTag()
    {
        Debug.Log("Tag");
        foreach(GameObject child in spline.GetComponent<ExempleRailling>().meshes)
        {
            child.tag = "terria";
        }

        spline = null;
    }

    void CreateRoad()
    {
        Debug.Log("OnDrag");
        //Touch touch;
        //if ( Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)//xác định số lần chạm hoặc không phải ngón tay chạm vào mh
        //{
        //    return;
        //}
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;
        log.text = "Before raycast";
        if(Frame.Raycast(Input.mousePosition.x, Input.mousePosition.y, raycastFilter, out hit))
        { 
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
                Debug.Log(hit.Pose.position);

            //if (hit.transform.tag == "terria")
            //{
                    position = hit.Pose.position;
                    log.text = "" + position;
                    position.y = 0;

                    if (spline == null)
                    {
                        GameObject newSpline = Instantiate(roadGeneretorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                        anchor.transform.parent = Container.Instance.transform;
                        newSpline.transform.parent = anchor.transform;
                        spline = newSpline.GetComponent<Spline>();
                        //spline.nodes[0].position = position;
                        //spline.nodes[1].position = position;
                        //Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.5f);
                        //spline.nodes[0].direction = firstDirection;
                        //spline.nodes[1].direction = firstDirection;
                    }
                    else
                    {
                        lastNode = spline.nodes.LastOrDefault();
                        lastNode.position.y = 0;
                        float distance = Vector3.Distance(lastNode.position, position);
                        Debug.Log("Distance: " + distance);
                        if (distance < space)
                            return;
                        else
                        {
                            float count = distance / space;
                            //Vector3 between = GetPoint(lastNode.position, position, 0.5f);
                            //int midpoint = (int)count / 2;
                            for (float i = 1; i < count; i++)
                            {
                                Vector3 _position = GetPoint(position, lastNode.position, i / count);
                                //if(roadHight.isOn)
                                //{
                                //    Vector3 _position2 = GetPoint(lastNode.position, position, i / count);

                                //}
                                AddNode(_position);
                                log.text = "AddNode" + i;
                                Debug.Log("AddNode" + i);

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
        _postion.y = 0;
        ////if(_postion.y - spline.nodes[0].position.y > 2f)
        ////    _postion.y += height;
        //_postion.y = _lastNode.position.y;
        Vector3 _direction = _postion;
        

        //_direction.y += (Mathf.Abs(_postion.y) - Mathf.Abs(_lastNode.position.y)) / f;
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

    private void _UpdateApplicationLifecycle()
    {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
        }
        else
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (m_IsQuitting)
        {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
        else if (Session.Status.IsError())
        {
            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
            m_IsQuitting = true;
            Invoke("_DoQuit", 0.5f);
        }
    }

    /// <summary>
    /// Actually quit the application.
    /// </summary>
    private void _DoQuit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Show an Android toast message.
    /// </summary>
    /// <param name="message">Message string to show in the toast.</param>
    private void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }
}
