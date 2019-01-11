using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICarTest : MonoBehaviour {

    public Vector3 centerOfMash;
    public List<Transform> path;
    public Transform pathground;
    public float maxSteer = 15;
    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    public Transform frontleftT, frontrightT, rearleftT, rearrightT;
    public int currentPathObj;
    public float distFromPath = 20;
    public float maxTorque = 120;
    public float currentSpeed;
    public float topSpeed = 150;
    public float decelerationSpeed = 25;
     Rigidbody rigidbody;
    public Vector3[] paths;
    public float count = 2;
    public NavMeshAgent _navMeshAgent;
    public Vector3 steerVector;
    public Vector3 nextPosition;
    public float distance;
    public float distanceStop = 1f;
    public float f;
    WheelFrictionCurve fFrictionL;
    WheelFrictionCurve sFrictionL;
    WheelFrictionCurve fFrictionR;
    WheelFrictionCurve sFrictionR;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerOfMash;
        GetPath();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
        nextPosition = transform.position;
         

    }
    //

    void GetPath()
    {
        Transform[] path_objs = pathground.GetComponentsInChildren<Transform>();

        foreach (Transform path_obj in path_objs)
        {
            if (path_obj != pathground)
                path.Add(path_obj);

        }
    }
    void Update()
    {

        MoveSpeedCar();
    }

    public void MoveSpeedCar()
    {

        Move();
        CheckPath();
        GetSteer();

        UpdateWheelPoses();

        //if (CheckState())
        //{
        //    _navMeshAgent.SetDestination(GetNext());

        //}

        //GetSteer(_navMeshAgent.nextPosition);
    }
    public void CheckPath()
    {
        // Vector3 navtargetVector = = path[currentPathObj].transform.position; 
        // steerVector = transform.InverseTransformPoint(navtargetVector);
        distance = Vector3.Distance(transform.position, steerVector);
        if (distance <= distFromPath)
        {
            currentPathObj++;
            if (currentPathObj >= path.Count)
                currentPathObj = 0;
        }
        steerVector = path[currentPathObj].transform.position;
    }
    // kiểm tra path tiếp theo để xe biết góc quay
    void GetSteer()
    {

        // Vector3 steerVector = transform.InverseTransformPoint(new Vector3(_navMeshAgent.nextPosition.x, transform.position.y, _navMeshAgent.nextPosition.z));

        f = Vector3.Distance(_navMeshAgent.nextPosition, transform.position);
        _navMeshAgent.SetDestination(steerVector);
        fFrictionL = wheelRL.forwardFriction;
        sFrictionL = wheelRL.sidewaysFriction;
        fFrictionR = wheelRR.forwardFriction;
        sFrictionR = wheelRR.sidewaysFriction;
        if (f < distanceStop)
        {
            
            fFrictionL.stiffness = 0.01f;
            sFrictionL.stiffness = 0.01f;

            wheelRL.forwardFriction = fFrictionL;
            wheelRL.sidewaysFriction = sFrictionL;

            
            fFrictionR.stiffness = 0.01f;
            sFrictionR.stiffness = 0.01f;
            Debug.Log(sFrictionR.stiffness);
            wheelRR.forwardFriction = fFrictionR;
            Debug.Log(wheelRR.forwardFriction.stiffness);
            
            wheelRR.sidewaysFriction = sFrictionR;
        }
        else
        {
            fFrictionL.stiffness = 1f;
            sFrictionL.stiffness = 1f;

            wheelRL.forwardFriction = fFrictionL;
            wheelRL.sidewaysFriction = sFrictionL;

            fFrictionR.stiffness = 1f;
            sFrictionR.stiffness = 1f;

            wheelRR.forwardFriction = fFrictionR;
            wheelRR.sidewaysFriction = sFrictionR;
        }

        
        nextPosition = transform.InverseTransformPoint(_navMeshAgent.nextPosition);

        float newSteer = maxSteer * (nextPosition.x / nextPosition.magnitude);

        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;

        //if (newSteer >= 1 || newSteer <= -1)
        //{

        //    maxTorque = 80f;
        //    Debug.Log("newSteer: " + newSteer + "currentSpeed: " + currentSpeed);
        //    WheelFrictionCurve fFrictionL = wheelRL.forwardFriction;
        //    WheelFrictionCurve sFrictionL = wheelRL.sidewaysFriction;
        //    fFrictionL.stiffness = 0.5f;
        //    sFrictionL.stiffness = 0.5f;

        //    wheelRL.forwardFriction = fFrictionL;
        //    wheelRL.sidewaysFriction = sFrictionL;

        //    WheelFrictionCurve fFrictionR = wheelRR.forwardFriction;
        //    WheelFrictionCurve sFrictionR = wheelRR.sidewaysFriction;
        //    fFrictionR.stiffness = 0.5f;
        //    sFrictionR.stiffness = 0.5f;

        //    wheelRR.forwardFriction = fFrictionR;
        //    wheelRR.sidewaysFriction = sFrictionR;

        //}
        //else
        //{
        //    maxTorque = 120f;
        //    Debug.Log("newSteer: " + newSteer + "currentSpeed: " + currentSpeed);
        //    WheelFrictionCurve fFrictionL = wheelRL.forwardFriction;
        //    WheelFrictionCurve sFrictionL = wheelRL.sidewaysFriction;
        //    fFrictionL.stiffness = 1f;
        //    sFrictionL.stiffness = 1f;

        //    wheelRL.forwardFriction = fFrictionL;
        //    wheelRL.sidewaysFriction = sFrictionL;

        //    WheelFrictionCurve fFrictionR = wheelRR.forwardFriction;
        //    WheelFrictionCurve sFrictionR = wheelRR.sidewaysFriction;
        //    fFrictionR.stiffness = 1f;
        //    sFrictionR.stiffness = 1f;

        //    wheelRR.forwardFriction = fFrictionR;
        //    wheelRR.sidewaysFriction = sFrictionR;
        //}



    }

    void Move()
    {
        currentSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;
        currentSpeed = Mathf.Round(currentSpeed);
        if (currentSpeed <= topSpeed)
        {
            wheelRL.motorTorque = maxTorque;
            wheelRR.motorTorque = maxTorque;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
        else
        {
            wheelRL.motorTorque = 0;
            wheelRR.motorTorque = 0;
            wheelRL.brakeTorque = decelerationSpeed;
            wheelRR.brakeTorque = decelerationSpeed;
        }

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
