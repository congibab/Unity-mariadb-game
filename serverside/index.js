// const dgram = require('dgram');
// const server = dgram.createSocket('udp4');

// server.on('error', (err) => {
//   console.log(`server error:\n${err.stack}`);
//   server.close();
// });

// server.on('message', (msg, senderInfo) =>{
//   console.log('Messages received : ' + msg);
//   server.send(msg, senderInfo.port, senderInfo.address, ()=>{
//     console.log(`Message sent to ${senderInfo.address}:${senderInfo.port}`);
//    });
// });

// server.on('listening', ()=>{
//   const address = server.address();
//   console.log(`server listening on ${address.address}:${address.port}`);
// });

// server.bind(3000, () => {
//   //server.addMembership('224.0.0.114'); 
// });

// var net = require('net');

// var server = net.createServer(function(socket){
//   console.log(socket.address().address + 'connected');

//   socket.on('data', function(data){
//     console.log('rcv : ' + data);
//     socket.write('welcome to server');
//   });

//   socket.on('close', function(){
//     console.log('client disconntedd');
//   });
// });

// server.on('error', function(err){
//   console.log('err : ' + err);
// });

// server.listen(5000, function(){
//   console.log('linsteing on 5000');
// });

var net = require('net'),
    sockets = [];
 
var server = net.createServer(function (client) {
    client.setEncoding('utf8');
    client.on('data', function (data) {
        for (var i = 0; i < sockets.length; i++) {
          sockets[i].write(data);
        }
    });
    client.on('error', function () {
        //console.log(`error`);
    });
    client.on('close', function () {
        sockets.pop();
        console.log('close ㅂㅂ');
    });

    client.on('timeout', function () {});
    client.write('hi hi');
    sockets.push(client);
});
 
server.on('error', function (error) {
 
});
server.listen(5000, function () {
 
    var serverInfo = server.address();
    var serverInfoJson = JSON.stringify(serverInfo);
    console.log('listen server : ' + serverInfoJson);
    server.on('close', function () {
        console.log('server closed.');
    });
    server.on('connection', function () {
        
        console.log(`테스트`);
    });
});