using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {

    // Use this for initialization
    private static Container _instance;

    void Awake () {
        _instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public static Container Instance
    {
        get
        {
                
            return _instance;
        }
    }
}
