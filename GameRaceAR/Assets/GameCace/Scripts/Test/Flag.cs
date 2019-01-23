using GoogleARCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	public List<GameObject> flags = new List<GameObject>();
    public GameObject flagPrefab;
    public GameObject curentFlag;
    public bool isActive;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            flags.Add(curentFlag);
            curentFlag = null;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            curentFlag = null;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
        TrackableHitFlags.FeaturePointWithSurfaceNormal;
        if (Frame.Raycast(0.5f, 0.5f, raycastFilter, out hit))
        {
            Debug.Log("flag");
            Debug.DrawLine(new Vector3(0.5f, 0.5f, 0), hit.Pose.position, Color.red, Mathf.Infinity);
            if(curentFlag == null)
            {
                curentFlag = Instantiate(flagPrefab, transform);
     
            }else
            {
                curentFlag.transform.position = hit.Pose.position;
            }
            
        }
            
    }

}
