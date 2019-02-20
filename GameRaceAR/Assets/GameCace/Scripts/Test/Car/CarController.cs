using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour {

    float moveHorizontal;
    float moveVertical;
    

    // Update is called once per frame
    void Update () {
        Gyroscope gyro = Input.gyro;
        moveHorizontal = gyro.gravity.x;
        moveVertical = gyro.gravity.y;

        Data data = new Data(Request.Move, SocketController.Instance.socketClient.roomId, null);
        data.msg = moveHorizontal  + ";" + moveVertical;
        SocketController.Instance.SendRequest(data);
    }


    void Start()
    {
        Input.gyro.enabled = true;
        
    }

    
}
