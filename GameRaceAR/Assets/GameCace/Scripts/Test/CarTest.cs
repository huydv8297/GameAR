using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTest : MonoBehaviour {
    [SerializeField]
    private float speed = 1f;
    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;

    private Rigidbody rb;
    public Text textH, textV;
    


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        //Quaternion dev = DeviceRotation.Get();

        //transform.rotation = dev;

        //rb.AddForce(transform.forward * speed, ForceMode.Acceleration);

        //rb.AddForce(transform.up * upSpeed, ForceMode.Acceleration);
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

        //Vector3 tilt = Input.acceleration;
        //textI.text = "acceleration: " + Input.acceleration.ToString();
        //tilt = Quaternion.Euler(90, 0, 0) * tilt;
        //rb.AddForce(tilt);
        //textt.text = "tilt: " + tilt.ToString();
        doMovement();


    }
    void doMovement()
    {
        float moveHorizontal;
        float moveVertical;
        if (Input.gyro.enabled == true)
        {
            Gyroscope gyro = Input.gyro;
            moveHorizontal = gyro.gravity.x;
            moveVertical = gyro.gravity.y;
            textH.text = "moveHorizontal: " + moveHorizontal.ToString();
            textV.text = "moveVertical: " + moveVertical.ToString();
        }
        else
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.time);
    }
}
