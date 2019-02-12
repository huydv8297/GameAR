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
    public float Checkdistance;
    WheelFrictionCurve fFrictionL;
    WheelFrictionCurve sFrictionL;
    WheelFrictionCurve fFrictionR;
    WheelFrictionCurve sFrictionR;
    public InterceptorController interceptor;
   

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
        UpdateWheelPoses();
    }

    public void MoveSpeedCar()
    {

        Move();
        CheckPath();
        GetSteer();

        

    }
    public void CheckPath()
    {
        
        distance = Vector3.Distance(transform.position, steerVector);
        if (distance <= distFromPath)
        {
            currentPathObj++;
            if (currentPathObj >= path.Count)
                currentPathObj = 0;
        }
        steerVector = path[currentPathObj].transform.position;

        if (steerVector == path[currentPathObj].transform.position)
        {
            // Debug.Log("steerVector " + steerVector);
            //StartCoroutine(WaitAndPrint());
        }
    }

    //private IEnumerator WaitAndPrint()
    //{
       
    //    yield return new WaitForSeconds(5);
    //    interceptor.checkpath();

    //}

    void GetSteer()
    {

        // Vector3 steerVector = transform.InverseTransformPoint(new Vector3(_navMeshAgent.nextPosition.x, transform.position.y, _navMeshAgent.nextPosition.z));

        Checkdistance = Vector3.Distance(_navMeshAgent.nextPosition, transform.position);
        _navMeshAgent.SetDestination(steerVector);
        fFrictionL = wheelRL.forwardFriction;
        sFrictionL = wheelRL.sidewaysFriction;
        fFrictionR = wheelRR.forwardFriction;
        sFrictionR = wheelRR.sidewaysFriction;
        if (Checkdistance < distanceStop)
        {
            
            fFrictionL.stiffness = 0.01f;
            sFrictionL.stiffness = 0.01f;

            wheelRL.forwardFriction = fFrictionL;
            wheelRL.sidewaysFriction = sFrictionL;

            
            fFrictionR.stiffness = 0.01f;
            sFrictionR.stiffness = 0.01f;
           
            wheelRR.forwardFriction = fFrictionR;
           
            
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
        Debug.Log(newSteer);

       
    }

    void Move()
    {
        currentSpeed = 2 * 22 / 7 * wheelRL.radius * wheelRL.rpm * 60 / 1000;

        currentSpeed = Mathf.Round(currentSpeed);
        //currentSpeed = Mathf.MoveTowards(currentSpeed, 128, 1 * Time.deltaTime);
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
