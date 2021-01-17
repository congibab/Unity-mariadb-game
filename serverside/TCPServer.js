const { v4: uuidv4 } = require('uuid');

var net = require('net');
var colors = require('colors');

var sockets = [];


var server = net.createServer();

server.on("connection", function (socket) {
    var remoteAddress = socket.remoteAddress + ":" + socket.remotePort;
    var thisClientId = uuidv4();
    sockets[thisClientId] = socket;
    //sockets.push(socket);

    var d = {
        type: 'init',
        id : thisClientId,
    };
    JSON_Send(socket, d);    
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
                case 'test':
                    JSON_Broadcast(JSONdata);
                    break;
                default:
                    console.log('type is not variable'.yellow);
                    break;
            }
            console.log("Data from %s: %s".cyan, remoteAddress, data);
            socket.write(data);
        } catch (error) {
            console.log('resived data type is not json'.yellow);
        }
    });

    socket.on('close', function () {
        delete sockets[thisClientId];
        //sockets.pop;
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
        console.log('data send fail)= this type is not JSON'.yellow);
    }
}

function JSON_Broadcast(data) {
    // Object.keys(sockets).forEach(function(key) {
    //     JSON_Send(sockets[key], data);
    // });
    Object.keys(sockets).forEach(key => JSON_Send(sockets[key], data));
}