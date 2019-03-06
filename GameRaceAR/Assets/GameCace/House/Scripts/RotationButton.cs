using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButton : MonoBehaviour
{
    public GameObject house;
    public float speed;
    bool isRotation;
    public void OnClickDown()
    {
        isRotation = true;
        Debug.Log("down");
    }

    public void OnClickUp()
    {
        isRotation = false;
        Debug.Log("up");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isRotation)
        {
            house.transform.Rotate(Vector3.up * Time.deltaTime * speed, Space.World);
            Debug.Log("Rotate");
        }
	}
}
