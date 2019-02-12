using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTest : MonoBehaviour {
    public float Speed;
    public float FinalSpeed = 10;

    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    public Transform frontleftT, frontrightT, rearleftT, rearrightT;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateWheelPoses();

    }
    public void SpeedT()
    {
        Speed = Mathf.MoveTowards(Speed, 0.1f, Time.deltaTime);
        transform.Translate(Vector3.forward * Speed);
        //txtSpeed.text = Speed.ToString("N1");
        wheelRL.motorTorque = 1800;
        wheelRR.motorTorque = 1800;
        wheelFL.motorTorque = 1800;
        wheelFR.motorTorque = 1800;
    }

    private void UpdateWheelPoses()
    {

        UpdatewheelPose(wheelFL, frontleftT);
        UpdatewheelPose(wheelFR, frontrightT);
        UpdatewheelPose(wheelRL, rearleftT);
        UpdatewheelPose(wheelRR, rearrightT);
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
