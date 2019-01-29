using System;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {
    SocketClient client;
    public Vector3 diretion;
    string diretionString;
    void Start () {
        client = SocketController.Instance.socketClient;
        SocketController.OnMove += OnMove;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += diretion;
        
    }

    public void OnButtonDown(string diretionString)
    {
        Data data = new Data(Request.Move, client.roomId , diretionString);
        SocketController.Instance.SendRequest(data);
    }

    public void OnMove(Data data)
    {
        Debug.Log("Onmove");

        string[] temp = data.msg.Split(',');
        diretion = new Vector3(Int32.Parse(temp[0]), Int32.Parse(temp[1]), Int32.Parse(temp[2]));
    }

    public void OnButtonUp()
    {
        diretionString = "0,0,0";
        Data data = new Data(Request.Move, client.roomId, diretionString);
        SocketController.Instance.SendRequest(data);
    }



    
}
