var net = require('net');
var colors = require('colors');

var sockets = [];

var server = net.createServer();

server.on("connection", function (socket) {
    var remoteAddress = socket.remoteAddress + ":" + socket.remotePort;
    console.log("new client connection is made %s".green, remoteAddress);

    sockets.push(socket);

    socket.on('data', function (d) {
        socket.setEncoding('utf8');
        console.log("Data from %s: %s".cyan, remoteAddress, d);
        socket.write(d);
    });

    socket.on('close', function () {
        sockets.pop();
        console.log("connection from %s closed", remoteAddress);
    });

    socket.on('error', function (err) {
        console.log("Connection %s error: %s", remoteAddress, err.message);
    });
});

server.listen(5000, function () {
    console.log("server listening to %j", server.address());
});