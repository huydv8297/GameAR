var app = require('express')();
var http = require('http').Server(app);
var io = require('socket.io')(http);

function getRandomInt(min, max) {
	return Math.floor(Math.random() * (max - min) + min);
}
function isExistRoom(id){
	if(io.sockets.adapter.rooms[id] != undefined)
		return true;
	return false;
}

function getNewRoomId(){
		var roomId = getRandomInt(10000, 99999);
		while(isExistRoom(roomId))
		{
			roomId = getRandomInt(10000, 99999);
		}
		return roomId;
}


function Response(responeCode,socket, msg = null)
{	
	
	console.log(responeCode + msg);
	if(responeCode === 'OnJoinedRoom')
	{
		console.log("Start...");
		io.to(msg).emit('Response', {
			code: responeCode,
			id: socket.userId,
			msg: msg
		});
		
	}else
	{
		socket.emit('Response', {
			code: responeCode,
			id: socket.userId,
			msg: msg
		});
	}
}


app.get('/', function(req, res){
  res.sendFile(__dirname + '/index.html');
});

var userId = 0;
io.on('connection', function(socket){
	socket.userId = userId ++;
	
	console.log('a user connected, user id: ' + socket.userId);
	
	
	socket.on('Request', function(data){
		console.log('GetRequest ' + data.code + ' from ' + socket.userId);
		switch(data.code)
		{
			case 'GetID':
				socket.to(socket.userId).emit('GetID', socket.userId);
				break;
			//Chat
			case 'Chat':
				console.log('message from user#' + socket.userId + ": " + data.msg);
				Response('Chat', socket, data.msg);
				break;
			//Create Room	
			case 'CreateRoom':
				var roomId = getNewRoomId();
				socket.join(roomId, function(){});
				console.log('User' + socket.userId + ' create room id : ' + roomId);
				Response('OnCreatedRoom', socket, roomId);
				break;
			//Join Room	
			case 'JoinRoom':
				if(!isExistRoom(data.msg)){
					Response('Error', socket, 'cant join room');
				}
				else{
					if(io.sockets.adapter.rooms[data.msg].length < 2)
					{
						socket.join(data.msg, function(){});
						console.log('User' + socket.userId + ' join room id : ' + data.msg);
						Response('OnJoinedRoom', socket, data.msg);
					}else{
						Response('Error', socket, ' full');
					}
				}
				break;
			default:
				
				break;
		}
	});

	//disconnect
	socket.on('disconnect', function () {
		console.log('A user disconnected');
	});
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});
