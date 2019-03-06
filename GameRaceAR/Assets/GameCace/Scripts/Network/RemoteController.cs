using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SocketController.OnClickDown += OnClickDown;
        SocketController.OnClickUp += OnClickUp;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            CustomCursor.OnMouseClickDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            CustomCursor.OnMouseClickUp();
        }
    }

    public void OnClickDown()
    {
        
        CustomCursor.OnMouseClickDown();
    }

    public void OnClickUp()
    {
        CustomCursor.OnMouseClickUp();
    }
}
