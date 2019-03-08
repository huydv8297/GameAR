using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour {

    public static Text log;
	void Start () {
        log = GetComponent<Text>();
    }
	

	public static void SetText (string s ) {
        log.text = s;
	}
}
