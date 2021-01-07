using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class TestTCPServer : MonoBehaviour
{
    string host = "localhost";
    int port = 5000;

    private Socket socket;
    private Thread thread;
    private bool _shouldStop = true;

    void Awake()
    {
        try
        {
            //IPアドレスやポートを設定している。
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

            //ソケットを作成
            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            //接続する。失敗するとエラーで落ちる。
            socket.Connect(remoteEP);
        }
        catch (Exception e)
        {
            _shouldStop = false;
            Destroy(this.gameObject);
            Debug.Log(e);
        }
    }
    void Start()
    {
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();

        //string st = "Hello World!";
        //SocketClient(st);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _shouldStop = false;
        }
    }

    private void ThreadMethod()
    {
        try
        {
            while (_shouldStop)
            {
                //Sendで送信している。
                byte[] msg = Encoding.UTF8.GetBytes("Hello world unity server");
                socket.Send(msg);

                //Receiveで受信している。
                byte[] bytes = new byte[1024];
                int bytesRec = socket.Receive(bytes);
                string data1 = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                Console.WriteLine(data1);
                Debug.Log(data1);
            }

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SocketClient(string st)
    {
        //IPアドレスやポートを設定している。
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

        //ソケットを作成
        socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //接続する。失敗するとエラーで落ちる。
        socket.Connect(remoteEP);

        //Sendで送信している。
        byte[] msg = Encoding.UTF8.GetBytes(st);
        socket.Send(msg);

        //Receiveで受信している。
        byte[] bytes = new byte[1024];
        int bytesRec = socket.Receive(bytes);
        string data1 = Encoding.UTF8.GetString(bytes, 0, bytesRec);
        Console.WriteLine(data1);
        Debug.Log(data1);

        //ソケットを終了している。
        //socket.Shutdown(SocketShutdown.Both);
        //socket.Close();
    }
}
