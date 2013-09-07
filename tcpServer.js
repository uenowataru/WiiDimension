// Load the TCP Library
net = require('net');
 
// Start a TCP Server
net.createServer(function (socket) {
 
  // Identify this client
  socket.name = socket.remoteAddress + ":" + socket.remotePort 
 
  // Send announce
  process.stdout.write(socket.name + " is on the server\n");
 
  // Handle incoming messages from clients.
  socket.on('data', function (data) {
    var message = "Love you C# (from js)\n";
    socket.write(message);
    socket.emit('news', { hello: 'world' });
    //process.stdout.write(message);
    process.stdout.write(data);

  });
 
  // Remove the client from the list when it leaves
  socket.on('end', function () {
     process.stdout.write(socket.name + " left\n");
  });
}).listen(8080);
 
// Put a friendly message on the terminal of the server.
console.log("Chat server running at port 8080\n");