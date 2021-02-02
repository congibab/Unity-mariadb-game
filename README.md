# UnityとNodejsのTCP通信練習

## 使用言語
Nodejs, C#

## 実行環境
Unity.2019.4.15f1  
Nodejs v14.15.0  
Vscode, Visual Studio 2019
## 使用module(Server)
uuid 8.3.2  
net 1.0.2  
colors 1.4.0  
express 4.17.1  
## 使用ライブラリ(Client)
System.IO  
System.Net  
System.Net.Sockets  
System.Threading  
## 制作期間
1が月（未完成）
## 開発人数
個人

# directory Path
```
├─build
├─networkgame(second)
│  ├─Assets
│  ├─Library
│  ├─obj
│  ├─packages
│  ├─Assembly-CSharp
│  └─networkgame(second)
└─serverside
    |─DataBase
    |─index.js
    |─package
    |─package-lock
    |─TCPServer.js
    |─UDPServer.js
    └─node_modules

```

# Program画面(Unity)


# Server source code（簡略）
``` javascript
//TCPServer.js
const { v4: uuidv4 } = require('uuid');

var net = require('net');
var colors = require('colors');
const { json } = require('express');
const { SSL_OP_NO_TICKET } = require('constants');

var sockets = [];


var server = net.createServer();

server.on("connection", function (socket) {
    var remoteAddress = socket.remoteAddress + ":" + socket.remotePort;
    var thisClientId = uuidv4();
    var d = {
        type: 'init',
        id: thisClientId,
        userType: ""
    };

    if (Object.keys(sockets).length === 0) d.userType = 'host';
    else d.userType = 'guest';

    sockets[thisClientId] = socket;

    JSON_Send(socket, d);

    console.log("new client connection is made %s, UUID : %s".green, remoteAddress, thisClientId);
    
    //クライアントからのメッセージ処理（JSON通信）
    socket.on('data', function (data) {
        socket.setEncoding('utf8');
        try {
            var JSONdata = JSON.parse(data);
            switch (JSONdata.type) {
                case 'init':
                    break;
                default:
                    console.log('type is not variable'.yellow);
                    break;
            }
            console.log("Data from %s: %s".cyan, remoteAddress, data);
        } catch (error) {
            console.log('resived data type is not json'.yellow);
        }
    });
```

# Client source code（簡略）
``` C#
//NetWorkeManager.cs
    static public NetworkManager instance;
    private TcpClient client;
    private NetworkStream nwStream;
    private Thread thread;

    private string _IPadress;
    private int _Port;

    public bool is_Connecting = false;
    
    void Awake()
    {
        Connect("localhost", "port number");
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
//=========================================
//中略
//=========================================
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
```