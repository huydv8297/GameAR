using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Container : MonoBehaviour {

    // Use this for initialization
    private static Container _instance;
    public GameObject skybox;
    public GameObject plane;
    public Slider roomSlider;
    void Awake () {
        _instance = this;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = roomSlider.value;
        transform.position = pos;
	}

    public static Container Instance
    {
        get
        {
            return _instance;
        }
    }
}
