using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

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
    Screen = 0,
    Remote = 1
}

public static class Request
{
    public const string Chat = "Chat";
    public const string SelectScreenType = "CreateRoom";
    public const string SelectRemoteType = "JoinRoom";
    public const string Move = "Move";
}

public static class Response
{
    public const string Chat = "Chat";
    public const string OnCreatedRoom = "OnCreatedRoom";
    public const string OnJoinedRoom = "OnJoinedRoom";
    public const string OnMove = "OnMove";
    public const string Error = "Error";
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


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        socketClient = new SocketClient();
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

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Response.OnCreatedRoom:
                screenText.text = socketClient.roomId;
                break;
            case Response.OnJoinedRoom:

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
            socket = null;
        }
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
        socket.On("Response", (data) => {
            string str = data.ToString();
            Debug.Log("GetResponse" + str);
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
                default:
                    break;
            }
        });
    }

    public void SelectScreen()
    {
        SendRequest(new Data(Request.SelectScreenType, socketClient.id, null));
    }

    public void SelectRemote()
    {
        SendRequest(new Data(Request.SelectRemoteType, socketClient.id, uiInput.text));
    }
}