using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour {

    public static Text log;
	void Start () {
        log = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Log(string msg)
    {
        log.text = msg;
    }
}
