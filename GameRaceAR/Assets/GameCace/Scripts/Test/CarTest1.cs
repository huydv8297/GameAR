using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarTest1 : MonoBehaviour {

    public bool smoothMotion = true;

    //This is how quickly we MoveTowards the input axis.
    public float smoothSpeed = 1f;

    //The maximum we want our input axis to reach. Setting this lower will rotate the platform less, and higher will be more.
    public float multiplier = 0.1f;

    //Variables, don't edit these.
    private float hSmooth = 0f;
    private float vSmooth = 0f;
    private float h;
    private float v;
    Rigidbody rb;
    public Text text;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Get Vertical and Horizontal axis from Input
        h = Input.GetAxis("Horizontal") * multiplier;
        v = Input.GetAxis("Vertical") * multiplier;
        //h = Input.acceleration.x;
        //v = Input.acceleration.y;
        //h = h * multiplier;
        //v = v * multiplier;




    }

    void FixedUpdate()
    {
        hSmooth = Mathf.MoveTowards(hSmooth, h, smoothSpeed * Time.deltaTime);
        vSmooth = Mathf.MoveTowards(vSmooth, v, smoothSpeed * Time.deltaTime);
        Vector3 movement = new Vector3(hSmooth, 0.0f, vSmooth);
        
        // Debug.Log(movement);
        rb.AddForce(movement);
        text.text = movement.ToString();
        //Rotate to match the new axis using EulerAngles.
        //if (!smoothMotion)
        //{
        //    Debug.Log("aaa"); 
        //    transform.rotation = Quaternion.Euler(new Vector3(h, 0f, v));
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(new Vector3(hSmooth, 0f, vSmooth));
        //}
    }
}
