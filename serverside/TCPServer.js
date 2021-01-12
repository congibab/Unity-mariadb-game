const { v4: uuidv4 } = require('uuid');

var net = require('net');
var colors = require('colors');

var sockets = [];


var server = net.createServer();

server.on("connection", function (socket) {
    var remoteAddress = socket.remoteAddress + ":" + socket.remotePort;
    var thisClientId = uuidv4();
    sockets[thisClientId] = socket;

    console.log(Object.keys(sockets).length);
    console.log("new client connection is made %s, UUID : %s".green, remoteAddress, thisClientId);

    socket.on('data', function (data) {
        socket.setEncoding('utf8');
        try {
            var JSONdata = JSON.parse(data);
            switch (JSONdata.type) {
                case 'init':                    
                    break;
                case 'test':
                    break;
                default:
                    break;
            }
            console.log("Data from %s: %s".cyan, remoteAddress, data);
            jsonBroadcast('test');
            //socket.write(data);
        } catch (error) {
            console.log('resived data type is not json'.yellow);
            JSON_Broadcast('test broadcast');
            //JSON_Send(socket, 'testtesttest');
        }
    });

    socket.on('close', function () {
        delete sockets[thisClientId];
        console.log("connection from %s closed, UUID : %s".yellow, remoteAddress, thisClientId);
    });

    socket.on('error', function (err) {
        console.log("Connection %s error: %s".red, remoteAddress, err.message);
    });
});

server.listen(5000, function () {
    console.log("server listening to %j".green, server.address());
});

//========================================================
//========================================================
//========================================================
function JSON_Send(socket, data){
    try {
        var dataJSON = JSON.stringify(data);
        socket.write(dataJSON);
        console.log('Send : %s'.blue, dataJSON);     
    } catch (error) {
        socket.write('test data');
        console.log('this type is not JSON'.yellow);
    }
}

function JSON_Broadcast(data) {
    sockets.forEach(function each(socket) {
        JSON_Send(socket, data);
    });
}