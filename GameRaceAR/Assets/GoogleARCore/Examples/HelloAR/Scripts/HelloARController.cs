//-----------------------------------------------------------------------
// <copyright file="HelloARController.cs" company="Google">
//
// Copyright 2017 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using System.Linq;


#if UNITY_EDITOR
    // Set up touch input propagation while using Instant Preview in the editor.
    using Input = InstantPreviewInput;
#endif

    /// <summary>
    /// Controls the HelloAR example.
    /// </summary>
    public class HelloARController : MonoBehaviour
    {
        /// <summary>
        /// The first-person camera being used to render the passthrough camera image (i.e. AR background).
        /// </summary>
        public Camera FirstPersonCamera;

        /// <summary>
        /// A prefab for tracking and visualizing detected planes.
        /// </summary>
        public GameObject DetectedPlanePrefab;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a plane.
        /// </summary>
        public GameObject AndyPlanePrefab;

        /// <summary>
        /// A model to place when a raycast from a user touch hits a feature point.
        /// </summary>
        public GameObject AndyPointPrefab;

        /// <summary>
        /// A game object parenting UI for displaying the "searching for planes" snackbar.
        /// </summary>
        public GameObject SearchingForPlaneUI;

        /// <summary>
        /// The rotation in degrees need to apply to model when the Andy model is placed.
        /// </summary>
        private const float k_ModelRotation = 180.0f;

        /// <summary>
        /// A list to hold all planes ARCore is tracking in the current frame. This object is used across
        /// the application to avoid per-frame allocations.
        /// </summary>
        private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

        /// <summary>
        /// True if the app is in the process of quitting due to an ARCore connection error, otherwise false.
        /// </summary>
        private bool m_IsQuitting = false;
        public bool isHaveCar = false;
        public bool showSearchingUI;
        public Spline spline;
        public SplineNode lastNode;
        public bool isCreate = false, isRoadLine = true;
        public int f = 10;
        public Vector3 position;
        public float space = 30f, height = 3f;
        public Vector3 scale = Vector3.zero;
        public GameObject roadGeneretorPrefab;
        public GameObject bridgeGeneretorPrefab;
        //public TerrianController terrianController;
        public GameObject bridgeGO;
        public Toggle road, bridge, river, roadLine, roadHight;
        public Text log;
        /// <summary>
        /// The Unity Update() method.
        /// </summary>
        public void Update()
        {
            _UpdateApplicationLifecycle();

            // Hide snackbar when currently tracking at least one plane.
            // 
            Session.GetTrackables<DetectedPlane>(m_AllPlanes);
            bool showSearchingUI = true;
            for (int i = 0; i < m_AllPlanes.Count; i++)
            {
                if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
                {
                    showSearchingUI = false;
                    break;
                }
            }

            SearchingForPlaneUI.SetActive(showSearchingUI);

            // If the player has not touched the screen, we are done with this update.
            //
            //Touch touch;
            //if (!isHaveCar && Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)//xác định số lần chạm hoặc không phải ngón tay chạm vào mh
            //{
            //    return;
            //}

            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                Debug.Log("KeyDown");
                log.text = "KeyDown";
                if (roadLine.isOn || roadHight.isOn)
                    isRoadLine = false;
                isCreate = true;
            }

            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                Debug.Log("KeyUp");
                log.text = "KeyUp";

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


            if (!isCreate)
                return;
            if (!road.isOn && !roadLine.isOn && !roadHight.isOn)
                return;
           // Vector3 currentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //isHaveCar = true;
            // Raycast against the location the player touched to search for planes.
            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;
            //Debug.Log(currentPosition);
            if (Frame.Raycast(Input.mousePosition.x, Input.mousePosition.y, raycastFilter, out hit))
            {
                //RaycastHit hit;
                //Ray ray = Camera.main.ViewportPointToRay(Input.mousePosition);
                //if (Physics.Raycast(ray, out hit);
                //{ 
                // Use hit pose and camera pose to check if hittest is from the
                // back of the plane, if it is, no need to create the anchor.
                position = hit.Pose.position;
                log.text = "" + position;
                Debug.DrawLine(Camera.main.transform.position, hit.Pose.position, Color.red);
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    log.text = "DetectedPlane";
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {
                    // Choose the Andy model for the Trackable that got hit.
                    //log.text = "instance";
                    GameObject prefab;
                    if (hit.Trackable is FeaturePoint)
                    {
                        prefab = AndyPointPrefab;
                    }
                    else
                    {
                        prefab = AndyPlanePrefab;
                    }

                    // Instantiate Andy model at the hit pose.
                     //var andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                    // Compensate for the hitPose rotation facing away from the raycast (i.e.camera).
                    //andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                    // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                    //world evolves.
                    

                    // Make Andy model a child of the anchor.
                    //andyObject.transform.parent = anchor.transform;
                    if (spline == null)
                    {
                        GameObject newSpline = Instantiate(roadGeneretorPrefab, hit.Pose.position, hit.Pose.rotation) as GameObject;
                        //var newAnchor = hit.Trackable.CreateAnchor(hit.Pose
                        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                        newSpline.transform.parent = anchor.transform;
                        log.text = "newSpline: " + newSpline.transform.position;
                        spline = newSpline.GetComponent<Spline>();
                        //spline.nodes[0].position = position;
                        //spline.nodes[1].position = position;
                        //Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.5f);
                        //spline.nodes[0].direction = firstDirection;
                        //spline.nodes[1].direction = firstDirection;
                    }
                   // else if (isRoadLine)
                   else
                    {
                        //log.text = "Addnote";
                        lastNode = spline.nodes.LastOrDefault();
                        position.y = 0;
                        float distance = Vector3.Distance(lastNode.position, position);
                        if (distance < space)
                        {
                            return;
                        }
                        else
                        {
                            float count = distance / space;
                            Debug.Log("distacne:" + distance + " count:" + count);
                            //Vector3 between = GetPoint(lastNode.position, position, 0.5f);
                            //int midpoint = (int)count / 2;
                            for (float i = 1; i < count; i++)
                            {
                                Vector3 _position = GetPoint(lastNode.position, position, i / count);
                                //if (roadHight.isOn)
                                //{
                                //    Vector3 _position2 = GetPoint(lastNode.position, position, i / count);

                                //}

                                AddNode(_position);
                                log.text = "Addnote" + i;
                                
                            }
                            //}
                        }

                    }

                }
            }
        }

        /// <summary>
        /// Check and update the application lifecycle.
        /// </summary>
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
    

    public void ChangeMode(int mode)
    {
    }

    //private void OnMouseDrag()
    //{
    //    if (road.isOn || roadLine.isOn || roadHight.isOn)
    //        CreateRoad();
    //    if (bridge.isOn)
    //        CreateBridge();
    //    if (river.isOn)
    //        CreateRiver();
    //}

    void SetTag()
    {
        Debug.Log("Tag");
        foreach (GameObject child in spline.GetComponent<ExempleRailling>().meshes)
        {
            child.tag = "terria";
        }

        spline = null;
    }

        //void CreateRoad()
        //{
        //    Debug.Log("OnDrag");
        //    if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)//xác định số lần chạm hoặc không phải ngón tay chạm vào mh
        //    {
        //        return;
        //    }

        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //    {

        //        if (hit.transform.tag == "terria")
        //        {
        //            log.text = "Raycast";
        //            if (spline == null)
        //            {
        //                GameObject newSpline = Instantiate(roadGeneretorPrefab, hit.Pose.position, hit.Pose.rotation) as GameObject;
        //                var anchor = hit.Trackable.CreateAnchor(hit.Pose);
        //                newSpline.transform.parent = anchor.transform;

        //                spline = newSpline.GetComponent<Spline>();
        //                spline.nodes[0].position = position;
        //                spline.nodes[1].position = position;
        //                Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.5f);
        //                spline.nodes[0].direction = firstDirection;
        //                spline.nodes[1].direction = firstDirection;
        //            }
        //            else if (isRoadLine)
        //            {
        //                lastNode = spline.nodes.LastOrDefault();
        //                float distance = Vector3.Distance(lastNode.position, position);
        //                if (distance < space)
        //                    return;

        //                float count = distance / space;
        //                Vector3 between = GetPoint(lastNode.position, position, 0.5f);
        //                int midpoint = (int)count / 2;
        //                for (float i = 1; i < count; i++)
        //                {
        //                    Vector3 _position = GetPoint(lastNode.position, position, i / count);
        //                    if (roadHight.isOn)
        //                    {
        //                        Vector3 _position2 = GetPoint(lastNode.position, position, i / count);

        //                    }

        //                    AddNode(_position);
        //                    log.text = "Addnote";
        //                    Debug.Log("AddNode");
        //                }
        //            }

        //        }
        //    }

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

    //void CreateRiver()
    //{
    //    CreateRoad();
    //}

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
    }
}
