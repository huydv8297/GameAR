using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContrller : MonoBehaviour {

    public Transform target;
    public float fixedY;
	void Start () {
        fixedY = transform.position.y;
	}
	
	void Update () {

        Vector3 newPosition = target.position;
        newPosition.y = fixedY;
        transform.position = newPosition;

        Quaternion newRotation = target.rotation;
        newRotation.x = 0;
        newRotation.z = 0;

        transform.rotation = newRotation;
    }
}
