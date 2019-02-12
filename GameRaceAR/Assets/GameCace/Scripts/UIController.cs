using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public Transform target;
    public Transform cursor;
    public float fixedY;

    public bool isMoveable;
	void Start () {
        fixedY = transform.position.y;
	}

    private void Update()
    {
        RaycastHit hit;
        //Ray ray = new Ray(cursor.position,transform.position -  cursor.position);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * 5000, Color.red, Mathf.Infinity);
        //int layerMask = 1 << 5;
        //int layerMask1 = ~layerMask;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("control"))
            {
                Debug.Log("hit");
                isMoveable = false;
            }
            
        }
        else
        {
            isMoveable = true;
        }
    }

    void LateUpdate () {
        if(isMoveable)
        {
            

            Quaternion newRotation = target.rotation;
            newRotation.x = 0;
            newRotation.z = 0;

            transform.rotation = newRotation;

            Vector3 newPosition = target.position;
            newPosition.y = fixedY;
            transform.position = newPosition;
            isMoveable = false;
        }
        
    }


}
