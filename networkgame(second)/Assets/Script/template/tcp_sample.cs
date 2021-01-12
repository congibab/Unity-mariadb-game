using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;


public class tcp_sample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TCPTest();
    }

    // Update is called once per frame
    void Update()
    {
        
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
