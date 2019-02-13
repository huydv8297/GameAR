using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTestPlayer : MonoBehaviour {
    public Transform car;
    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    public Transform wheelFLTrans, wheelFRTrans, wheelRLTrans, wheelRRTrans;
    
    public float decelerationSpeed = 25;
    public float maxTorque = 50;
    public float currentSpeed;
    public float topSpeed = 190;
    public float maxReverseSpeed = 50;
    public float throttle = 0;
    public bool braked = false;
    public float maxBrakeTorque = 100;
    
    private float mySidewayFriction;
    private float myForwardFriction;
    private float slipSidewayFriction;
    private float slipForwardFriction;
    public AudioSource audiocar;

   
    Rigidbody rigidbody;
    WheelFrictionCurve fFrictionRL;
    WheelFrictionCurve sFrictionRL;
    WheelFrictionCurve fFrictionRR;
    WheelFrictionCurve sFrictionRR;

    public float a, b;

    float x, y, z;

    public Texture2D speedOmeterPointer;
    public Texture2D speedOmeterDial;
    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = new Vector3(0,0,0);
        audiocar = GetComponent<AudioSource>();
        // SetValues();
    }
	
	void SetValues()
    {
        //ma sat trươt
        myForwardFriction = wheelRR.forwardFriction.stiffness;

        mySidewayFriction = wheelRR.sidewaysFriction.stiffness;

        slipForwardFriction = 0.04f;
        slipSidewayFriction = 0.25f;
    }

    private void FixedUpdate()
    {
        Control();
        UpdateWheelPoses();
       
    }

    void Control()
    {
        currentSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        currentSpeed = Mathf.Round(currentSpeed);
        
        
        if (currentSpeed < topSpeed ){
            var m_vertical = Input.GetAxis("Vertical");
            wheelRR.motorTorque = maxTorque * m_vertical;
            wheelRL.motorTorque = maxTorque * m_vertical;
          
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
        }

        if(Input.GetButton("Vertical") == false)
        {
            wheelRL.brakeTorque = decelerationSpeed;
            wheelRR.brakeTorque = decelerationSpeed;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }

        var currentSteerAngle = 20 * Input.GetAxis("Horizontal");
        Debug.Log(currentSteerAngle);
        wheelFR.steerAngle = currentSteerAngle ;
        wheelFL.steerAngle = currentSteerAngle ;
        Debug.Log(currentSpeed);
    }

    private void UpdateWheelPoses()
    {
        UpdatewheelPose(wheelFL, wheelFLTrans);
        UpdatewheelPose(wheelFR, wheelFRTrans);
        UpdatewheelPose(wheelRL, wheelRLTrans);
        UpdatewheelPose(wheelRR, wheelRRTrans);
    }

    private void UpdatewheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 2 - 250 , Screen.height - 300, 500, 300), speedOmeterDial);
        float speedFactor = currentSpeed / topSpeed;
        float rotationAngle;
        if (currentSpeed > 1)
        {
            audiocar.Play();
        }
        if (currentSpeed >= 0)
        {
            rotationAngle = Mathf.Lerp(0, 180, speedFactor);
        }
        else
        {
            rotationAngle = Mathf.Lerp(0, 180, -speedFactor);
        }

        GUIUtility.RotateAroundPivot(rotationAngle, new Vector2(Screen.width / 2  , Screen.height));
        GUI.DrawTexture(new Rect(Screen.width / 2 - 250  , Screen.height - 250, 500, 500), speedOmeterPointer);

    }

}
