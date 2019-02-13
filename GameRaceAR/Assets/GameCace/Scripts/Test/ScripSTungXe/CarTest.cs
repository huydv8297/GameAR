using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CarTest : MonoBehaviour {
    [SerializeField]
    private float speed = 20f;
    
    private Rigidbody rb;
    //public Text textH, textV;
    private float m_motorFprce;

    public float motorFprce = 50;
    /// <summary>
    /// /////////////////////
    /// </summary>
    private float moveHorizontal, moveVertical;
    private float m_steeringAngle;
    public float maxSteerAngle = 30;

    /// <summary>
    /// //////////////////
    /// </summary>
    public WheelCollider frontleftW, frontrightW, rearleftW, rearrightW;
    public Transform frontleftT, frontrightT, rearleftT, rearrightT;

    public float currentSpeed;
    public float Topspeed = 150;
    public float maxTorque = 120;
    float trang;
    float tung;

    //public Texture2D speedOmeterPointer;
    //public Texture2D speedOmeterDial;
    //public AudioSource audiocar;





    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
        //audiocar = GetComponent<AudioSource>();

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


        Gyroscope gyro = Input.gyro;
        moveHorizontal = gyro.gravity.x;
        moveVertical = gyro.gravity.y;
        control();
    }

    //void doMovement()
    //{
      
    //    if (Input.gyro.enabled == true)
    //    {
    //        currentSpeed = Mathf.MoveTowards(currentSpeed, 1f, Time.deltaTime);
           
           
    //        tung = moveVertical * 1;
    //        trang = moveHorizontal * 1;
    //        textH.text = "moveHorizontal: " + moveHorizontal.ToString();
    //        textV.text = "moveVertical: " + moveVertical.ToString();

    //        rearrightW.motorTorque = maxTorque * tung;
    //        rearleftW.motorTorque = maxTorque * tung;
    //    }
    //    else
    //    {

    //         currentSpeed = Mathf.MoveTowards(currentSpeed, 1f, Time.deltaTime);
    //        moveHorizontal = Input.GetAxis("Horizontal");
    //        moveVertical = Input.GetAxis("Vertical");
    //         tung = moveVertical * currentSpeed;
    //        trang = moveHorizontal * currentSpeed;
    //        rearrightW.motorTorque =  tung * 10;
    //        rearleftW.motorTorque =  tung * 10;
    //        Debug.Log(tung);
    //    }
    //    //  Vector3 movement = new Vector3(0.0f, 0.0f, tung);

    //    // rb.AddForce(movement * speed );
    //    //transform.Translate(new Vector3(0.0f, 0, rearrightW.motorTorque));
    //    //transform.Translate(new Vector3(0.0f, 0, rearleftW.motorTorque));
    //    //transform.Translate(new Vector3(0.0f, 0, tung));
    //    //transform.Rotate(new Vector3(0.0f, trang, 0.0f));
    //}
    //private IEnumerator WaitAndPrint()
    //{

    //    yield return new WaitForSeconds(1);
        
    //    doMovement();

    //}

    void control()
    {
        currentSpeed = 2 * 22 / 7 * rearleftW.radius * rearleftW.rpm * 60 / 1000;
        currentSpeed = Mathf.Round(currentSpeed);
        if (currentSpeed < Topspeed)
        {
            tung = moveVertical * 1;
            trang = moveHorizontal * 1;
            //textH.text = "currentSpeed: " + currentSpeed.ToString();
            //textV.text = "tung: " + tung.ToString();

            rearrightW.motorTorque = maxTorque * tung;
            rearleftW.motorTorque = maxTorque * tung;
        }

        var currentSteerAngle = 20 * moveHorizontal;
        Debug.Log(currentSteerAngle);
        frontrightW.steerAngle = currentSteerAngle;
        frontleftW.steerAngle = currentSteerAngle;
        
    }
    /// <summary>
    /// //////////////////////////
    /// </summary>
    private void UpdateWheelPoses()
    {
        UpdatewheelPose(frontleftW, frontleftT);
        UpdatewheelPose(frontrightW, frontrightT);
        UpdatewheelPose(rearleftW, rearleftT);
        UpdatewheelPose(rearrightW, rearrightT);
    }

    private void UpdatewheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void Reload()
    {
        SceneManager.LoadScene("test scene");
    }

    //public void OnGUI()
    //{
    //    GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 300, 500, 300), speedOmeterDial);
    //    float speedFactor = currentSpeed / Topspeed;
    //    float rotationAngle;
    //    if (currentSpeed > 1)
    //    {
    //        audiocar.Play();
    //    }
    //    if (currentSpeed >= 0)
    //    {
    //        rotationAngle = Mathf.Lerp(0, 180, speedFactor);
    //    }
    //    else
    //    {
    //        rotationAngle = Mathf.Lerp(0, 180, -speedFactor);
    //    }

    //    GUIUtility.RotateAroundPivot(rotationAngle, new Vector2(Screen.width / 2, Screen.height));
    //    GUI.DrawTexture(new Rect(Screen.width / 2 - 250, Screen.height - 250, 500, 500), speedOmeterPointer);

    //}
}
