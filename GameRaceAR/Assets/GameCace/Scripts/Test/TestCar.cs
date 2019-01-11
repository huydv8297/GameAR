using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCar : MonoBehaviour {

    [SerializeField]
    private Vector3 axis;

    [SerializeField]
    private float spinSpeed = 10f;
   
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(axis, spinSpeed);
        
	}
    
}
