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

    private bool is_Connecting = false;
    private Thread thread;
    private string _IPadress;
    private int _Port;

    void Awake()
    {

    }

    void Start()
    {

    }

    private void ThreadMethod()
    {
        try
        {
            Send();
            //receive
            while (is_Connecting)
            {
                byte[] receiveBytes = new byte[client.ReceiveBufferSize];
                int bytesRead = nwStream.Read(receiveBytes, 0, client.ReceiveBufferSize);
                string receivedString = Encoding.ASCII.GetString(receiveBytes, 0, bytesRead);
                print("Message received from the server \n " + receivedString);
            }
        }
        catch (Exception e)
        {
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
            Debug.Log(e);
            is_Connecting = false;
        }
    }

    public void DisConnect()
    {
        client.Close();
        is_Connecting = false;
    }

    public void Send()
    {
        try
        {
            nwStream = client.GetStream();
            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
            nwStream.Write(sendBytes, 0, sendBytes.Length);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            is_Connecting = false;
        }
    }

    void TCPTest()
    {
        TcpClient client = new TcpClient();
        try
        {
            client.Connect("127.0.0.1", 5000);

            //send
            NetworkStream nwStream = client.GetStream();
            byte[] sendBytes = Encoding.ASCII.GetBytes("Hello, from the client");
            nwStream.Write(sendBytes, 0, sendBytes.Length);

            //receive
            byte[] receiveBytes = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(receiveBytes, 0, client.ReceiveBufferSize);
            string receivedString = Encoding.ASCII.GetString(receiveBytes, 0, bytesRead);

            print("Message received from the server \n " + receivedString);
        }
        catch (Exception e)
        {
            print("Exception thrown " + e.Message);
        }
    }
}