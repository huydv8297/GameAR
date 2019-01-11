using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InterceptorController : MonoBehaviour {

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    public GameObject player, Interceptor;
     public AICarTest aicartest;


	// Use this for initialization
	void Start () {
        target = player.transform;
       
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        Debug.Log(aicartest.currentSpeed);
        if (aicartest.currentSpeed >= aicartest.topSpeed)
        {
            
            agent.SetDestination(target.position);
        }
        else
        {
          
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
