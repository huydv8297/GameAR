using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SimpleCarController : MonoBehaviour {

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private float m_motorFprce;
   
    public float maxSteerAngle = 30;
    public float motorFprce = 50;
    public bool CheckVer;
    public WheelCollider frontleftW, frontrightW, rearleftW, rearrightW;
    public Transform frontleftT, frontrightT, rearleftT, rearrightT;
    public Text text;

    private void FixedUpdate()
    {
        
        UpdateWheelPoses();
    }

    //public void GetInput()
    //{
    //    m_horizontalInput = Input.GetAxisRaw("Horizontal");
    //    m_verticalInput = Input.GetAxisRaw("Vertical");
    //    Debug.Log(m_verticalInput+ " GetInput" + m_horizontalInput);
    //}

  

    public void steerduong(int b)
    {          
            m_steeringAngle = maxSteerAngle * b;
            frontleftW.steerAngle = m_steeringAngle;
            frontrightW.steerAngle = m_steeringAngle;
    }
    public void steeram(int b2)
    {
        m_steeringAngle = maxSteerAngle * b2;
        frontleftW.steerAngle = m_steeringAngle;
        frontrightW.steerAngle = m_steeringAngle;
    }

    public void Accelerateduong(int a)
    {
           
            m_motorFprce = a * motorFprce;
        
            rearleftW.motorTorque = m_motorFprce;
            rearrightW.motorTorque = m_motorFprce;
        Debug.Log("" + m_motorFprce * Time.deltaTime);
    }

    public void Accelerateam(int a2)
    {     
        m_motorFprce = a2 * motorFprce;
        rearleftW.motorTorque = m_motorFprce;
        rearrightW.motorTorque = m_motorFprce;
    }

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



}
