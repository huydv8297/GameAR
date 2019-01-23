using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using UnityEngine.UI;

public class InstanceSkybox : MonoBehaviour
{

    public bool isInstance;
    public GameObject skyboxPrefab;
    public Text text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
                    anchor.transform.parent = Container.Instance.transform;
                    //GameObject box = Instantiate(skyboxPrefab, hit.Pose.position - new Vector3(0, 5, 0), Quaternion.identity, anchor.transform) as GameObject;
                    skyboxPrefab.transform.parent = anchor.transform;
                    skyboxPrefab.transform.localPosition = Vector3.zero;
                    skyboxPrefab.SetActive(true);
                    text.text = "instance skybox" + skyboxPrefab.transform.position;
                    isInstance = true;
                }

            }

        }


    }
}
