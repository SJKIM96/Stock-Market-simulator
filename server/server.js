var net = require('net');

var port = 8000;


//create server
var server = net.createServer();

server.on("connection", function (socket) {
   var rAddress = socket.remoteAddress + ":" + socket.remotePort;
   console.log("REGISTER By: %s" , rAddress);
   socket.write('SME/TCP-1.0 OK');
   // socket.once("unregister", function () {});
   socket.on("data", function (data) {
       console.log("Data from %s %s", rAddress, data);
   });
   // socket.on("listSellOrders", function () {});
   // socket.on("listBuyOrders", function () {});
   // socket.on("sellOrder", function () {});
   // socket.on("buyOrder", function () {});

    socket.on("error", function (err) {
       console.log("Connection %s error : %s", rAddress, err.message);
    });
});
// server.on("unregister", function (user) {
//     console.log("unregister user by: " + user);
// });
// server.on("listCompanies", function () {
//     console.log("listCompanies");
// });
// server.on("listSellOrders", function () {
//     console.log("listSellOrders");
// });
// server.on("listBuyOrders", function () {
//     console.log("listBuyOrders");
// });
// server.on("sellOrder", function (data) {
//     console.log("sellOrder recieved with " + data);
// });
// server.on("buyOrder", function (data) {
//     console.log("buyOrder recieved with " + data);
// });


// START THE SERVER
// =============================================================================
server.listen(port, function () {
    console.log("Stock Exchange Server available on %j", server.address());
});