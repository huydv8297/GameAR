using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InterceptorController : MonoBehaviour {

    //public float lookRadius = 10f;
    //private int count = 0;
    //Transform target;
    //NavMeshAgent agent;
    //public GameObject player;
    //public AICarTest aicartest;
    //public float countspeednav = 4;
    public WheelCollider wheelFL, wheelFR, wheelRL, wheelRR;
    public Transform frontleftT, frontrightT, rearleftT, rearrightT;

    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;
    public AICarTest aitestcar;
 


    private void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
      
        
    }

    private void Update()
    {
       
       
        if (_navMeshAgent == null)
        {
            Debug.LogError("Error");
        }
        else
        {
            UpdateWheelPoses();
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            
             Vector3 targetVector = _destination.transform.position;
             _navMeshAgent.SetDestination(targetVector);

            wheelRL.motorTorque = 120;
            wheelRR.motorTorque = 120;
            wheelFL.motorTorque = 120;
            wheelFR.motorTorque = 120;

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




    // Use this for initialization
    //   void Start () {
    //       target = player.transform;

    //       agent = GetComponent<NavMeshAgent>();
    //}

    //// Update is called once per frame
    //void Update () {
    //       UpdateWheelPoses();
    //   }

    //   private void OnDrawGizmosSelected()
    //   {
    //       Gizmos.color = Color.red;
    //       Gizmos.DrawWireSphere(transform.position, lookRadius);
    //   }
    //   public void checkpath()
    //   {
    //       float distance = Vector3.Distance(target.position, transform.position);
    //       //Debug.Log(target.position + "target.position");
    //       //Debug.Log(transform.position + "transform.position");

    //       if (aicartest.currentSpeed >= aicartest.topSpeed)
    //       {
    //           // Debug.Log("currentSpeed " + aicartest.currentSpeed + " topSpeed " + aicartest.topSpeed);
    //           agent.SetDestination(target.position);

    //           agent.speed = countspeednav;

    //           wheelRL.motorTorque = 120;
    //           wheelRR.motorTorque = 120;
    //           wheelFL.motorTorque = 120;
    //           wheelFR.motorTorque = 120;


    //       }

    //   }

}
