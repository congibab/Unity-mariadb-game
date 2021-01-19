using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class NetworkManager : MonoBehaviour
{
    static public NetworkManager instance;
    private TcpClient client;
    private NetworkStream nwStream;
    private Thread thread;

    private string _IPadress;
    private int _Port;

    public bool is_Connecting = false;

    [SerializeField]
    UIManager uiManager;

    void Awake()
    {
        Connect("localhost", "5000");
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        instance = null;
        client.Close();
        is_Connecting = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    private void ThreadMethod()
    {
        try
        {
            Send("client answer");
            //receive
            while (is_Connecting)
            {
                byte[] receiveBytes = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(receiveBytes, 0, client.ReceiveBufferSize);
                string receivedString = Encoding.ASCII.GetString(receiveBytes, 0, bytesRead);
                print("Message received from the server \n " + receivedString);

                try
                {
                    json_data user = json_data.CreateFromJSON(receivedString);
                    switch (user.type)
                    {
                        case "init":
                            clientStatus.OwnID = user.id;
                            clientStatus.userType = user.userType;

                            user.type = "anotherUUID";
                            user.id = clientStatus.OwnID;
                            Send(json_data.CreateToJSON(user));
                            break;
                        case "anotherUUID":
                            clientStatus.AnotherID = user.id;
                            break;

                        case "GameStart":
                            clientStatus.is_gameRun = true;
                            break;
                        case "turnChange":
                            
                            break;
                        default:
                            break;
                    }

                }
                catch (Exception e) { Debug.Log(e); }
            }
        }
        catch (Exception e)
        {
            is_Connecting = false;
            instance = null;
            client.Close();
            Debug.Log(e);
        }
    }

    public void Connect(string ip, string port)
    {
        client = new TcpClient();
        try
        {
            instance = this;
            _IPadress = ip;
            _Port = int.Parse(port);
            client.Connect(_IPadress, _Port);
            thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
            is_Connecting = true;
        }

        catch (Exception e)
        {
            Destroy(this.gameObject);
            Debug.Log(e);
        }
    }

    public void DisConnect()
    {
        client.Close();
        instance = null;
        is_Connecting = false;
    }

    public void Send(json_data data)
    {
        try
        {
            string message = json_data.CreateToJSON(data);
            nwStream = client.GetStream();
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            nwStream.Write(sendBytes, 0, sendBytes.Length);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            is_Connecting = false;
        }
    }
    public void Send(string message)
    {
        try
        {
            nwStream = client.GetStream();
            byte[] sendBytes = Encoding.ASCII.GetBytes(message);
            nwStream.Write(sendBytes, 0, sendBytes.Length);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            is_Connecting = false;
        }
    }

}