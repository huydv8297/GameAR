using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    // Use this for initialization
    public float speed = 10f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed;
        }
    }
}
