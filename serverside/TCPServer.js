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
    //JSON_Broadcast(socket, d);

    console.log("new client connection is made %s, UUID : %s".green, remoteAddress, thisClientId);

    socket.on('data', function (data) {
        socket.setEncoding('utf8');
        //JSON_Broadcast('broadcast');
        //=============================
        //evect hendler function
        try {
            var JSONdata = JSON.parse(data);
            switch (JSONdata.type) {
                case 'init':

                    break;
                case 'anotherUUID':
                    get_another_uuid(socket, JSONdata);
                    break;
                case 'GameStart':
                    JSON_ALL_Broadcast({type : JSONdata.type});
                    break;
                case 'turnChange':

                    break;
                default:
                    console.log('type is not variable'.yellow);
                    break;
            }
            console.log("Data from %s: %s".cyan, remoteAddress, data);
        } catch (error) {
            console.log('resived data type is not json'.yellow);
            //console.log(' %s'.yellow, error);
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
function JSON_Send(socket, data) {
    try {
        var dataJSON = JSON.stringify(data);
        socket.write(dataJSON);
        console.log('Send : %s'.blue, dataJSON);
    } catch (error) {
        console.log('data send fail)= this type is not JSON'.yellow);
    }
}

function JSON_ALL_Broadcast(data) {
    Object.keys(sockets).forEach(key => JSON_Send(sockets[key], data));
}

function JSON_Broadcast(socket, data) {
    Object.keys(sockets).forEach(function each(key) {
        if (socket === sockets[key]) {
            return;
        }
        JSON_Send(sockets[key], data);
    });
}


function get_another_uuid(socket, JSONdata) {
    var d = {
        type: JSONdata.type,
        id: JSONdata.id
    }
    JSON_Broadcast(socket, d);

    for (var key in sockets) {
        if (key !== JSONdata.id) {
            d.id = key;
            JSON_Send(socket, d);
        }
    }
}