using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine.SceneManagement;

public class Data
{
    public string code;
    public string id;
    public string msg;

    public Data(string _code, string _id, string _msg)
    {
        code = _code;
        id = _id;
        msg = _msg;
    }

    public Data()
    { }

    public override string ToString()
    {
        return "{code: " + code + " , id: " + id + " , msg: " + msg + "}";
    }
};
public enum Type : int
{
    None = 0,
    Screen = 1,
    Remote = 2
}

public static class Request
{
    public const string Chat = "Chat";
    public const string SelectScreenType = "CreateRoom";
    public const string SelectRemoteType = "JoinRoom";
    public const string Move = "OnMove";
    public const string ClickDown = "OnClickDown";
    public const string ClickUp = "OnClickUp";
    public const string Exit = "Exit";

}

public static class Response
{
    public const string Chat = "Chat";
    public const string OnCreatedRoom = "OnCreatedRoom";
    public const string OnJoinedRoom = "OnJoinedRoom";
    public const string OnMove = "OnMove";
    public const string OnClickDown = "OnClickDown";
    public const string OnClickUp = "OnClickUp";
    public const string Error = "Error";
    public const string Exit = "Exit";
}


public class SocketClient
{
    public string id;
    public Type type;
    public string roomId;
}


public class SocketController : MonoBehaviour
{
    public string serverURL = "http://localhost:3000";
    public static SocketController Instance;
    public InputField uiInput = null;
    public Text uiChatLog = null;
    public SocketClient socketClient;
    public GameObject screenPanel;
    public GameObject remotePanel;
    public GameObject selectPanel;
    public string state;
    protected Socket socket = null;
    protected List<string> chatLog = new List<string>();
    public Text screenText;
    public GameObject test;
    public string roomId;
    public static Action<Data> OnMove = delegate { };
    public static Action OnClickDown = delegate { };
    public static Action OnClickUp = delegate { };
    public static Action OnExit = delegate {  };
    public GameObject remoteScene;
    public GameObject screenScene;
    public GameObject selectScene;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        socketClient = new SocketClient();

        OnExit += Destroy;
    }

    void Destroy()
    {
        DoClose();
    }

    // Use this for initialization
    void Start()
    {
        //ProcessStartInfo startInfo = new ProcessStartInfo("node.exe");
        //startInfo.WindowStyle = ProcessWindowStyle.Minimized;

        //startInfo.Arguments = "C:\\Users\\Huydv\\Desktop\\test-server\\index.js";
        //Process.Start(startInfo);
        DoOpen();
    }

    void Update()
    {
        switch (state)
        {
            case Response.OnCreatedRoom:
                screenText.text = socketClient.roomId;
                break;
            case Response.OnJoinedRoom:
               
                if (socketClient.type == Type.Screen)
                {
                    screenScene.SetActive(true);
                    selectScene.SetActive(false);
                }
                if (socketClient.type == Type.Remote)
                {
                    remoteScene.SetActive(true);
                    selectScene.SetActive(false);
                }
                state = "Playing";
                break;
            default:
                break;
        }
        lock (chatLog)
        {
            if (chatLog.Count > 0)
            {
                string str = uiChatLog.text;
                foreach (var s in chatLog)
                {
                    str = str + "\n" + s;
                }
                uiChatLog.text = str;
                chatLog.Clear();
            }
        }
    }
    void DoOpen()
    {
        if (socket == null)
        {
            socket = IO.Socket(serverURL);
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                lock (chatLog)
                {
                    chatLog.Add("Socket.IO connected.");
                }
            });

            socket.On("GetID", (userId) =>
            {
                socketClient.id = (string)userId;
            });

            GetRespone();

        }
    }

    void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
            Debug.Log("Disconnect");
            socket = null;
        }

        Application.Quit();
    }

    public void SendRequest(Data data)
    {
        Debug.Log("SendRequest:" + data.ToString());
        if (socket != null)
        {
            socket.Emit("Request", JObject.FromObject(data));
        }

    }

    

    public void GetRespone()
    {
        //socket.On("onclickdown", () =>
        //{
        //    Debug.Log("onclickdown");
        //    OnClickDown();
        //});

        //socket.On("onclickup", () =>
        //{
        //    Debug.Log("onclickup");
        //    OnClickUp();
        //});

        //socket.On("exit", () =>
        //{
        //    Debug.Log("exit");
        //    Application.Quit();
        //});

        socket.On("Response", (data) => {
            string str = data.ToString();
            Debug.Log("GetResponse" + str);
            lock (chatLog)
            {
                chatLog.Add(str);
            }

            Data responsetData = JsonConvert.DeserializeObject<Data>(str);
            switch (responsetData.code)
            {
                case Response.Chat:
                    string strChatLog = "user#" + responsetData.id + ": " + responsetData.msg;

                    lock (chatLog)
                    {
                        chatLog.Add(strChatLog);
                    }
                    break;
                case Response.Error:
                    Debug.Log("Error:  " + responsetData.msg);
                    break;
                case Response.OnCreatedRoom:
                    socketClient.type = Type.Screen;
                    socketClient.id = responsetData.id;
                    socketClient.roomId = responsetData.msg;
                    state = Response.OnCreatedRoom;
                    
                    break;
                case Response.OnJoinedRoom:
                    if(socketClient.type == Type.None)
                        socketClient.type = Type.Remote;
                   
                    socketClient.id = responsetData.id;
                    socketClient.roomId = responsetData.msg;
                    roomId = responsetData.msg;
                    state = Response.OnJoinedRoom;
                    Debug.Log(roomId);
                    break;
                case Response.OnMove:
                    Debug.Log("OnMove");
                    OnMove(responsetData);
                    break;
                case Response.OnClickDown:
                    Debug.Log("OnClickDown");
                    OnClickDown();
                    break;
                case Response.OnClickUp:
                    Debug.Log("OnClickUp");
                    OnClickUp();
                    break;
                case Response.Exit:
                    Debug.Log("Exit");
                    OnExit();
                    break;
                default:
                    break;
            }
        });

        
    }

    //public void SendExit()
    //{
    //    if (socket != null)
    //    {
    //        socket.Emit("exit", socketClient.roomId);
    //        Debug.Log("Send exit:" + socketClient.roomId);
    //    }
    //}

    //public void SendClickDown(string roomId)
    //{
    //    if (socket != null)
    //    {
    //        socket.Emit("onclickdown", roomId);
    //        Debug.Log("Send OnClickDown:" + roomId.ToString());
    //    }
    //}

    //public void SendClickUp(string roomId)
    //{
    //    if (socket != null)
    //    {
    //        socket.Emit("onclickup", roomId);
    //        Debug.Log("Send OnClickUp:" + roomId.ToString());
    //    }
    //}

    public void SelectScreen()
    {
        SendRequest(new Data(Request.SelectScreenType, socketClient.id, null));
    }

    public void SelectRemote()
    {
        SendRequest(new Data(Request.SelectRemoteType, socketClient.id, uiInput.text));
    }
}