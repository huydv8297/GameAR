using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEngine : MonoBehaviour {
    public Transform path;
    private List<Transform> nodes;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL,wheelFR, wheelRL, wheelRR;

    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centerOfMass;
    public bool isBraking = false;

    public int currectNode = 0;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPostion = new Vector3 (0f,0f, 0.3f);
    public float frontsideSensorPosition = 0.15f;
    public float frontSensorAngle = 30;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }
	
	// Update is called once per frame
	private void FixedUpdate () {
        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
        Braking();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStarPos = transform.position;
        sensorStarPos += transform.forward * frontSensorPostion.z;
        sensorStarPos += transform.up * frontSensorPostion.y;
        

        if(Physics.Raycast(sensorStarPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStarPos, hit.point);
        }
        
        
        //right

        sensorStarPos += transform.right * frontsideSensorPosition;
        if (Physics.Raycast(sensorStarPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStarPos, hit.point);
        }
        

        //right angle
        
        if (Physics.Raycast(sensorStarPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStarPos, hit.point);
        }

        //left

        sensorStarPos -= transform.right * frontsideSensorPosition * 2;
        if (Physics.Raycast(sensorStarPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStarPos, hit.point);
        }

        //left angle
        
        if (Physics.Raycast(sensorStarPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStarPos, hit.point);
        }
       

    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currectNode].position);
        
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
        
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if (currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;

        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
    }

    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currectNode].position) < 2f)
        {
            if (currectNode == nodes.Count - 1)
            {
                currectNode = 0;
            }
            else
            {
                currectNode++;
            }
        }
    }

    public void Braking()
    {
        if (isBraking)
        {
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
           
        }
        else
        {
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }
}
