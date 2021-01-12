using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCP_Client : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream nwStream;
    private Thread thread;

    private string _IPadress;
    private int _Port;

    public bool is_Connecting = false;

    void Awake()
    {

    }

    void Start()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    private void ThreadMethod()
    {
        try
        {
            Send("return from the client");
            //receive
            while (is_Connecting)
            {
                byte[] receiveBytes = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(receiveBytes, 0, client.ReceiveBufferSize);
                string receivedString = Encoding.ASCII.GetString(receiveBytes, 0, bytesRead);
                print("Message received from the server \n " + receivedString);

                try
                {
                    UserJSON user = UserJSON.CreateFromJSON(receivedString);
                    switch (user.type)
                    {
                        case "init":
                            Debug.Log("this is type test");
                            break;
                        case "":
                            break;
                        default:
                            break;
                    }

                } catch(Exception e) { }
            }
        }
        catch (Exception e)
        {
            is_Connecting = false;
            client.Close();
            Debug.Log(e);
        }
    }

    public void Connect(string ip, string port)
    {
        client = new TcpClient();
        try
        {
            _IPadress = ip;
            _Port = int.Parse(port);
            client.Connect(_IPadress, _Port);
            thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
            is_Connecting = true;
        }

        catch (Exception e)
        {
            client.Close();
            Debug.Log(e);
            is_Connecting = false;
        }
    }

    public void DisConnect()
    {
        client.Close();
        is_Connecting = false;
    }

    public void Send(UserJSON data)
    {
        try
        {
            string message = UserJSON.CreateToJSON(data);
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