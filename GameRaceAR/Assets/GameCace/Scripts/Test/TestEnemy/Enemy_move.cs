using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_move : MonoBehaviour {

    private bool _patrol;

    RaycastHit _lookhit;

    private SpeedTest tung;
    // Use this for initialization
    void Start()
    {
        _patrol = true;
        tung = GetComponent<SpeedTest>();
    }

    // Update is called once per frame
    void Update () {
        tung.SpeedT();
    }
}
