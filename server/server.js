var net = require('net');
var moment = require('moment');

var port = 8000;

var session = 0;

//default val for company
var clients = [];
var companies = {
    "companies": []
};
var ms ={
    "name":"Microsoft Corporation",
    "symbol": "MSFT",
    "openPrice": 36,
    "currentPrice": 35,
    "closedPrice" : 35
};
var fb = {
    "name":"Facebook, Inc.",
    "symbol": "FB",
    "openPrice": 80.1,
    "currentPrice": 80.2,
    "closedPrice" : 80.15
};
var apple = {
    "name":"Apple Inc.",
    "symbol": "AAPL",
    "openPrice": 105.22,
    "currentPrice": 104.22,
    "closedPrice" : 104.22
};


//save buyorder, sellorder of each company

var SellOrder = {
    "MSFT": [],
    "FB": [],
    "AAPL": []
};

var BuyOrder = {
    "MSFT": [],
    "FB": [],
    "AAPL": []
};
//temp val
var order = {
    "size": 100,
    "price": 35,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
var order1 = {
    "size": 100,
    "price": 35,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
var order2 = {
    "size": 100,
    "price": 35,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
SellOrder.MSFT.push(order);
SellOrder.AAPL.push(order1);
SellOrder.FB.push(order2);

var Border = {
    "size": 100,
    "price": 15,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
var Border1 = {
    "size": 100,
    "price": 15,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
var Border2 = {
    "size": 100,
    "price": 15,
    "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
};
BuyOrder.MSFT.push(Border);
BuyOrder.AAPL.push(Border1);
BuyOrder.FB.push(Border2);
//end of dummy var

companies.companies.push(ms);
companies.companies.push(fb);
companies.companies.push(apple);

console.log(JSON.stringify(companies));

function addBuyorder(Sorder, Border, order, company) {

    let solved = false;
    let went = false;
    if (Sorder.length > 0)
    {
        //let sale;
        Sorder.forEach( function (sell_order){
            // if we have sell order with the same price do the trasaction and add it into Transaction list
            if (order.price >= sell_order.price)
            {
                console.log("order price > cur price ");
                if (sell_order.size === order.size && !solved)
                {
                    solved = true;
                    //sale = new BuyOrder(order.price, order.size);
                    let index = Sorder.indexOf(sell_order);
                    if (index > -1)
                        Sorder.splice(index,1);
                    company.currentPrice = order.price;

                }
                else if (sell_order.size > order.size && !solved)
                {
                    solved = true;
                    // do partial transacqion
                    // do not add to buyOrders list
                    let remainingSize = sell_order.size - order.size;
                    //sale = new BuyOrder(order.price, order.size);
                    sell_order.size = remainingSize;
                    company.currentPrice = order.price;

                    //                       market.Notify();
                }
                else if (sell_order.size < order.size && !solved)
                {
                    // do partial transacqion
                    // remove sellOrder from the sellOrders list
                    // add to buyOrders for the remaining size in buyOrders list
                    order.size = order.size - sell_order.size;
                    let index = Sorder.indexOf(sell_order);
                    if (index > -1)
                        Sorder.splice(index,1);
                    company.currentPrice = order.price;
                    addBuyorder(Sorder, Border, order, company);
                }
            }
            else if (!solved && !went)
            {
                went = true;
                Border.push(order);
                Border.sort(compare);

            }
        });
    }
    else
    {
        Border.push(order);
        Border.sort(compare);
    }
}

function addSellorder(Sorder, Border, order, company) {
    let solved = false;
    let went = false;
    if (Border.length> 0)
    {
        Border.forEach( function (buy_order)
        {
            // if we have buy order with the same price do the trasaction and add it into Transaction list
            if (buy_order.price >= order.price)
            {
                if (buy_order.size === order.size && !solved)
                {
                    solved = true;
                    // do full transacqion
                    // do not add to sellOrders list
                    // remove buyOrder from the buyOrders list

                    let index = Border.indexOf(buy_order);
                    if (index > -1)
                        Border.splice(index,1);
                    company.currentPrice = order.price;
                    return;
                }
                else if (buy_order.size > order.size && !solved)
                {
                    solved = true;
                    // do partial transacqion
                    // do not add to sellOrders list
                    // update buyOrder size to the remaining size in buyOrders list
                    buy_order.size = buy_order.size - order.size;
                    company.currentPrice = order.price;
                    return;
                }
                else if (buy_order.size < order.size && !solved)
                {
                    // do partial transacqion
                    // remove buyOrder from the buyOrders list
                    // add to sellOrders for the remaining size in sellOrders list
                    order.size = order.size - buy_order.size;

                    let index = Border.indexOf(buy_order);
                    if (index > -1)
                        Border.splice(index,1);
                    company.currentPrice = order.price;
                    addSellorder(Sorder, Border, order, company);
                }
            }
            else if (!solved && !went)
            {
                went = true;
                Sorder.push(order);
                Sorder.sort(Scompare);
                return;
            }
        });
    }
    else
    {
        Sorder.push(order);
        Sorder.sort(Scompare);
    }
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function compare(a,b) {
    if (a.price > b.price)
        return -1;
    if (a.price < b.price)
        return 1;
    return 0;
}
function Scompare(a,b) {
    if (a.price < b.price)
        return -1;
    if (a.price > b.price)
        return 1;
    return 0;
}
//create server
var server = net.createServer();


server.on("connection", function (socket) {
    var curSession = session++;
    var Cseq;
    var SSeq = getRandomInt(0,100000);
    var rAddress = socket.remoteAddress + ":" + socket.remotePort;
    let id;
   // socket.once("unregister", function () {});
   socket.on("data", function (data) {
       let arr = data.toString('ascii').split(" ");
       if (id == null)
           id = arr[2];
       console.log("\nTrader %s from %s is connected ",id, rAddress);

       console.log("\n%s requests:\n%s" , id, data);
       switch (arr[0]){
           case "register":
               Cseq = parseInt(arr[4]);
               clients.push(socket);
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq++ +' Session: '+ curSession);
               console.log("\nserver responds:\nSME/TCP-1.0 OK\nCSeq: " + Cseq + ' Session: '+ curSession +' ');
               // socket.write('\nNOTIFY SME/TCP-1.0 \nSSeq: '+ SSeq);
               break;
           case "listCompanies":
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession +' Data: ' + JSON.stringify(companies));
               console.log("\nserver responds:\nSME/TCP-1.0 OK \nCSeq: "+ Cseq +' Session: '+ curSession +' Data: ' + JSON.stringify(companies));
               break;
           case "unregister":
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession);
               console.log('\nserver responds:\nSME/TCP-1.0 OK \nCSeq: '+ Cseq +' Session: '+ curSession +"\n\n"+id +"'s session is closed");

               var index = clients.indexOf(socket);
               if (index > -1)
                   clients.splice(index,1);
               socket.end();
               break;
           case "listSellOrders":
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession +' Data: ' + JSON.stringify(SellOrder));
               console.log('\nserver responds:\nSME/TCP-1.0 OK \nCSeq: '+ Cseq +' Session: '+ curSession +' Data: ' + JSON.stringify(SellOrder));
               break;
           case "listBuyOrders":
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession +' Data: ' + JSON.stringify(BuyOrder));
               console.log('\nserver responds:\nSME/TCP-1.0 OK \nCSeq: '+ Cseq+' Session: '+ curSession +' Data: ' + JSON.stringify(BuyOrder));
               break;
           case "sellOrder":
               let _data = data.toString('ascii').split('Data: ');
               let order =JSON.parse(_data[1]);
               let orderC =Object.keys(JSON.parse(_data[1]));
               if (orderC[0] === 'MSFT'){
                   let newOrder = {
                       "size": order.MSFT.size,
                       "price": order.MSFT.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   //notify other clients
                   console.log(JSON.stringify(newOrder));
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Sell MSFT SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addSellorder(SellOrder.MSFT, BuyOrder.MSFT, newOrder, ms);
               }
               else if (orderC[0] === 'AAPL'){
                   let newOrder = {
                       "size": order.AAPL.size,
                       "price": order.AAPL.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   //notify other clients
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Buy AAPL SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addSellorder(SellOrder.AAPL, BuyOrder.AAPL, newOrder, apple);
               }
               else if (orderC[0] === 'FB'){
                   let newOrder = {
                       "size": order.FB.size,
                       "price": order.FB.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   //notify other clients
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Buy AAPL SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addSellorder(SellOrder.FB, BuyOrder.FB, newOrder, fb);
               }

               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession);
               console.log('\nserver responds:\nSME/TCP-1.0 OK \nCSeq: '+ Cseq+' Session: '+ curSession);

               break;
           case "buyOrder":
               let __data = data.toString('ascii').split('Data: ');
               let _order =JSON.parse(__data[1]);
               let _orderC =Object.keys(JSON.parse(__data[1]));
               if (_orderC[0] === 'MSFT'){
                   let newOrder = {
                       "size": _order.MSFT.size,
                       "price": _order.MSFT.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   console.log(JSON.stringify(newOrder));
                   //notify other clients
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Buy AAPL SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addBuyorder(SellOrder.MSFT, BuyOrder.MSFT, newOrder, ms);
               }
               else if (_orderC[0] === 'AAPL'){
                   let newOrder = {
                       "size": _order.AAPL.size,
                       "price": _order.AAPL.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   console.log(JSON.stringify(newOrder));
                   //notify other clients
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Buy AAPL SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addBuyorder(SellOrder.AAPL, BuyOrder.AAPL, newOrder, apple);
               }
               else if (_orderC[0] === 'FB'){
                   let newOrder = {
                       "size": _order.FB.size,
                       "price": _order.FB.price,
                       "timestamp" : moment().format("DD/MM/YYYY hh:mm:ss A")
                   };
                   console.log(JSON.stringify(newOrder));
                   //notify other clients
                   clients.forEach(function (client){
                       if (client !== socket){
                           console.log("notified to client: "+ id);
                           client.write('notify Buy AAPL SME/TCP-1.0 OK \nData: ' + JSON.stringify(newOrder));
                       }
                   });
                   addBuyorder(SellOrder.FB, BuyOrder.FB, newOrder, fb);
               }
               socket.write('SME/TCP-1.0 OK \nCSeq: '+ Cseq+++' Session: '+ curSession);
               console.log('\nserver responds:\nSME/TCP-1.0 OK \nCSeq: '+ Cseq+' Session: '+ curSession);
               break;
           default:
               socket.write('SME/TCP-1.0 FAIL');
               socket.end();
               console.log(id + "closed with unexpected error");
               break;
       }

   });
    socket.on("error", function (err) {
       console.log("Connection %s error : %s", rAddress, err.message);
    });
});



// START THE SERVER
// =============================================================================
server.listen(port, function () {
    console.log("Stock Exchange Server available on %j\n", server.address());
});