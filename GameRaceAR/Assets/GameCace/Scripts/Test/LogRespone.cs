using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogRespone : MonoBehaviour {

    public Text log;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        log.text = SocketController.Instance.uiChatLog.text;
    }
}
