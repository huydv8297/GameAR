﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TestNavmesh : MonoBehaviour {

    public NavMeshAgent agent;
    public Transform target;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.position);
	}
}
