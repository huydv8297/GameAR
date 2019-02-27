//-----------------------------------------------------------------------
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
        public GameObject StarPrefab;

        /// <summary>
        /// The Anchor model that will represent the anchor in the scene.
        /// </summary>
        public GameObject AnchorPrefab;
        public GameObject roadPrefab;
        public Spline spline;
        public List<Spline> splines = new List<Spline>();
        public SplineNode lastNode;
        float space = 0.1f;
        /// <summary>
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
        [Command]
        public void CmdSpawnStar(Vector3 position, Quaternion rotation)
        {
            // Instantiate Star model at the hit pose.
            var starObject = Instantiate(StarPrefab, position, rotation);

            // Spawn the object in all clients.
            NetworkServer.Spawn(starObject);
        }
       [Command]
        public void CmdCreateRoad(Vector3 position, Quaternion rotation)
        {

        
            if (spline == null)
            {
                GameObject newSpline = Instantiate(roadPrefab, position, rotation) as GameObject;
                NetworkServer.Spawn(newSpline);
               // log.SetString("instantiate" + position);
                spline = newSpline.GetComponent<Spline>();
                splines.Add(spline);
                spline.nodes[0].position = position;
                spline.nodes[1].position = position;

                Vector3 firstDirection = GetPoint(spline.nodes[0].position, spline.nodes[1].position, 0.5f);
                spline.nodes[0].direction = firstDirection;
                spline.nodes[1].direction = firstDirection;
            }
            else
            {
               // log.SetString("else 1");
                lastNode = spline.nodes.LastOrDefault();

                float distance = Vector3.Distance(lastNode.position, position);

                if (distance < space)
                {
                   log.SetString("else hai"+distance + "spcace" + space);
                    return;
                }
                    
                else
                {
                    float count = distance / space;
                   // log.SetString("else 3");
                    for (float i = 1; i < count; i++)
                    {
                        Vector3 _position = GetPoint(lastNode.position, position, i / count);
                       log.SetString("else" +_position);
                        AddNode(_position);


                    }
                }
               // log.SetString("instantiate1");

            }
        }




        Vector3 GetPoint(Vector3 start, Vector3 end, float percent)
        {
            return (start + percent * (end - start));
        }

        public void SetTag()
        {
            spline = null;
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

        }
    }
}
