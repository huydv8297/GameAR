using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKButton : MonoBehaviour {

    public void OnMouseDown()
    {
        SocketController.Instance.SendRequest(new Data(Request.ClickDown, SocketController.Instance.socketClient.roomId, null));
        //SocketController.Instance.SendClickDown(SocketController.Instance.socketClient.roomId);
    }

    public void OnMouseUp()
    {
        SocketController.Instance.SendRequest(new Data(Request.ClickUp, SocketController.Instance.socketClient.roomId, null));
        //SocketController.Instance.SendClickUp(SocketController.Instance.socketClient.roomId);
    }
}
