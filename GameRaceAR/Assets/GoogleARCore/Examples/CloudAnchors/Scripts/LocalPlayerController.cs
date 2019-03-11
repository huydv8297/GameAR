﻿//-----------------------------------------------------------------------
// <copyright file="LocalPlayerController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
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

namespace GoogleARCore.Examples.CloudAnchors
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Networking;

    /// <summary>
    /// Local player controller. Handles the spawning of the networked Game Objects.
    /// </summary>
    public class LocalPlayerController : NetworkBehaviour
    {
        /// <summary>
        /// The Star model that will represent networked objects in the scene.
        /// </summary>

        /// <summary>
        /// The Anchor model that will represent the anchor in the scene.
        /// </summary>
        public GameObject AnchorPrefab;
        public GameObject skyBox;
        public GameObject roadPrefab;
        public GameObject nodeRoad;
        public Spline spline;
        public List<Spline> splines = new List<Spline>();
        public SplineNode lastNode;
        float space = 5f;
        /// <summary>
        /// 
        /// The Unity OnStartLocalPlayer() method.
        /// </summary>
        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            // A Name is provided to the Game Object so it can be found by other Scripts, since this is instantiated as
            // a prefab in the scene.
            gameObject.name = "LocalPlayer";
        }

        /// <summary>
        /// Will spawn the origin anchor and host the Cloud Anchor. Must be called by the host.
        /// </summary>
        /// <param name="position">Position of the object to be instantiated.</param>
        /// <param name="rotation">Rotation of the object to be instantiated.</param>
        /// <param name="anchor">The ARCore Anchor to be hosted.</param>
        public void SpawnAnchor(Vector3 position, Quaternion rotation, Component anchor)
        {

            // Instantiate Anchor model at the hit pose.
            var anchorObject = Instantiate(AnchorPrefab, position, rotation);

            // Anchor must be hosted in the device.
            anchorObject.GetComponent<AnchorController>().HostLastPlacedAnchor(anchor);

            // Host can spawn directly without using a Command because the server is running in this instance.
            NetworkServer.Spawn(anchorObject);


        }

        /// <summary>
        /// A command run on the server that will spawn the Star prefab in all clients.
        /// </summary>
        /// <param name="position">Position of the object to be instantiated.</param>
        /// <param name="rotation">Rotation of the object to be instantiated.</param>

        public void CmdSpawnStar()
        {
            GameObject newBox = Instantiate(skyBox, Vector3.zero, Quaternion.identity) as GameObject;
            NetworkServer.Spawn(newBox);
        }

        public void SpawnNode(GameObject go)
        {
       
                NetworkServer.Spawn(go);
            
        }
        public void CreateRoad(Vector3 position, Quaternion rotation)
        {
            Debug.Log("bắt đầu hàm vẽ");
            if (spline == null)
            {
                Debug.Log("khoi tao duong");
                GameObject newSpline = Instantiate(roadPrefab,Vector3.zero, rotation) as GameObject ;
                spline = newSpline.GetComponent<Spline>();
             
                splines.Add(spline);
                spline.nodes[0].position = position;
                spline.nodes[1].position = position;
                Vector3 firstDirection = spline.nodes[0].position + 0.5f * (spline.nodes[1].position - spline.nodes[0].position);
                spline.nodes[0].direction = firstDirection;
                spline.nodes[1].direction = firstDirection;
                NetworkServer.Spawn(newSpline);
            }
            else
            {
                lastNode = spline.nodes.LastOrDefault();
                float distance = Vector3.Distance(lastNode.position, position);
                if (distance < space)
                    return;
                else
                {
                    float count = distance / space;
                    for (float i = 1; i < count; i++)
                    {
                        Debug.Log("ve"+i);
                        Vector3 __position = lastNode.position + (i / count) * (position - lastNode.position);
                        SplineNode _lastNode = spline.nodes.LastOrDefault();
                        Vector3 _postion = __position;
                        Vector3 _direction = _lastNode.position + 1.2f * (_postion - _lastNode.position);
                        SplineNode _node = new SplineNode(_postion, _direction);
                        spline.AddNode(_node);                      
                        if (spline.nodes[0].position == spline.nodes[1].position)
                        {
                            spline.nodes[0].direction = spline.nodes[0].position + 0.5f * (spline.nodes[2].position - spline.nodes[0].position);
                            spline.RemoveNode(spline.nodes[1]);
                        }

                    }
                  
                }

            }
          
        }

       
        public void SetTag()
        {                 
            spline = null;
        }
        

        //public void CmdGetPoint(Vector3 start, Vector3 end, float percent,Vector3 vt3)
        //{
        //    vt3= start + percent * (end - start);
        //}



        //void CmdAddNode(Vector3 position)
        //{
        //    SplineNode _lastNode = spline.nodes.LastOrDefault();
        //    Vector3 _postion = position;

        //    Vector3 _direction = _lastNode.position + 1.2f * (_postion - _lastNode.position);


        //    SplineNode _node = new SplineNode(_postion, _direction);
        //    spline.AddNode(_node);

        //    if (spline.nodes[0].position == spline.nodes[1].position)
        //    {
        //        spline.nodes[0].direction = spline.nodes[0].position + 0.5f * (spline.nodes[2].position - spline.nodes[0].position);
        //        spline.RemoveNode(spline.nodes[1]);
        //    }

        //}
    }
}
