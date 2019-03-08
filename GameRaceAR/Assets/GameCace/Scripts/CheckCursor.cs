using GoogleARCore.Examples.CloudAnchors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCursor : MonoBehaviour  {

  



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CustomCursor.HitTransform(transform))
            CloudAnchorsExampleController.isPlane = true;
        else
            CloudAnchorsExampleController.isPlane = false;
    }
}
