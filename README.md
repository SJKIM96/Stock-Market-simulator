# StockMarket Simulator

StockMarket simulator using C# and Node.js.
Construct using observer pattern to send/recieve message.
Send information using it as JSON format

##for server 
1. npm install
2. nodemon server
3. Whenever client joins, send current information and listen to the socket.
4. Whenever buy/sell slip order is recieved, update info and broadcast it to other clients

##for client
1. During initialization send client information and ip,port# to register and recieve current stock market information
2. When client finishes recieving all company information, use sub thread to recieve message.
3. When the message is recieved, invoke main thread to update information. 

# **note**
* Server does not check if it is valid user
* Client does not validate if server response is correct.

## testing
used [Packet Sender](https://packetsender.com/) to serve as other client.
send buy/sellOrder using packet Sender
