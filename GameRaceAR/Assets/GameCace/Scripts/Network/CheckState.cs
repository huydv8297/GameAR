using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : MonoBehaviour {

	void Update () {
		if(SocketController.Instance.state == Response.OnJoinedRoom)
        {
            gameObject.SetActive(false);
        }
	}
}
