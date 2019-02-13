using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_Animation : MonoBehaviour {

    public float rotateSpeed = 5f;

    private void Start()
    {
        setTransform();
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotateSpeed * Time.fixedDeltaTime, 0, Space.Self);
    }

    private void setTransform()
    {
        transform.localPosition = new Vector3(0, 0, -1.5f);
        //transform.localScale = new Vector3(1, 1, 1);
        //transform.localRotation = new Quaternion(5, 0, -5, 0);
    }

}
