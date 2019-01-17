using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class InstanceSkybox : MonoBehaviour {

    public bool isInstance;
    public GameObject skyboxPrefab;
    public Text text;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Session.Status == SessionStatus.Tracking)
        {
            if (!isInstance)
            {

                

                TrackableHit hit;
                TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;
                if (Frame.Raycast(0.5f, 0.5f, raycastFilter, out hit))
                {
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    GameObject box = Instantiate(skyboxPrefab, hit.Pose.position, Quaternion.identity, anchor.transform) as GameObject;
                    box.transform.parent = anchor.transform;
                    text.text = "instance skybox" + box.transform.position;
                    isInstance = true;
                }
                Debug.Log("Instance skybox");
                    
            }

        }

        
	}
}
