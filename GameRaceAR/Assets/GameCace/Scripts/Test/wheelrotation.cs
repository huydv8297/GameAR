using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelrotation : MonoBehaviour {
    public GameObject FontWheelsleft, FontWheelsRight, RearWheelsleft, RearWheelsRight;
    public Transform aaa;

    float _StartSpeed = 0;
    float _UpdateSpeed = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        MoveSpeed();
        if (_UpdateSpeed > 0)
        {
            FontWheelsleft.transform.Rotate(new Vector3(5 * Time.time, 0f, 0f));
            FontWheelsRight.transform.Rotate(new Vector3(5 * Time.time, 0f, 0f));
            RearWheelsleft.transform.Rotate(new Vector3(5 * Time.time, 0f, 0f));
            RearWheelsRight.transform.Rotate(new Vector3(5 * Time.time, 0f, 0f));
        }else if(_UpdateSpeed == 0)
        {
            FontWheelsleft.transform.Rotate(new Vector3(0, 0f, 0f));
            FontWheelsRight.transform.Rotate(new Vector3(0, 0f, 0f));
            RearWheelsleft.transform.Rotate(new Vector3(0, 0f, 0f));
            RearWheelsRight.transform.Rotate(new Vector3(0, 0f, 0f));
        }else if(_UpdateSpeed < 0)
        {
            FontWheelsleft.transform.Rotate(new Vector3(-5 * Time.time, 0f, 0f));
            FontWheelsRight.transform.Rotate(new Vector3(-5 * Time.time, 0f, 0f));
            RearWheelsleft.transform.Rotate(new Vector3(-5 * Time.time, 0f, 0f));
            RearWheelsRight.transform.Rotate(new Vector3(-5 * Time.time, 0f, 0f));
        }
    }

    void MoveSpeed()
    {
        _StartSpeed = Mathf.MoveTowards(_StartSpeed, _UpdateSpeed,  Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 0.5f * _StartSpeed));

        if (Input.GetKeyDown(KeyCode.W))
        {
            _UpdateSpeed = 0.5f;

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            _UpdateSpeed = 0;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _UpdateSpeed = -5f;

        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Rotate(new Vector3(0, 20f * Time.deltaTime, 0));
            FontWheelsleft.transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0f));
            FontWheelsRight.transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0f));


        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Rotate(new Vector3(0, -20f * Time.deltaTime, 0));
            FontWheelsleft.transform.Rotate(new Vector3(0, -10 * Time.deltaTime, 0f));
            FontWheelsRight.transform.Rotate(new Vector3(0, -10 * Time.deltaTime, 0f));

        }
    }
}
