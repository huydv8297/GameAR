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


function Response(responeCode,socket, roomId = null, msg = null)
{	
	
	
	if(responeCode != 'OnCreatedRoom')
	{
		
		io.to(roomId).emit('Response', {
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
	
	console.log(responeCode + msg);
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
				Response('Chat', socket, null, data.msg);
				break;
			//Create Room	
			case 'CreateRoom':
				var roomId = getNewRoomId();
				socket.join(roomId, function(){});
				console.log('User' + socket.userId + ' create room id : ' + roomId);
				Response('OnCreatedRoom', socket, roomId, roomId);
				break;
			//Join Room	
			case 'JoinRoom':
				if(!isExistRoom(data.msg)){
					Response('Error', socket, null, 'cant join room');
				}
				else{
					if(io.sockets.adapter.rooms[data.msg].length < 2)
					{
						socket.join(data.msg, function(){});
						console.log('User' + socket.userId + ' join room id : ' + data.msg);
						Response('OnJoinedRoom', socket, data.msg, data.msg);
					}else{
						Response('Error', socket, null,' full');
					}
				}
				break;
			case 'Move':
				console.log('message from user#' + socket.userId + ": " + data.msg);
				Response('OnMove', socket,data.id, data.msg);
				break;
			default:
				
				break;
		}
	});

	//disconnect
	socket.on('disconnect', function () {
		console.log('User ' + socket.userId + ' disconnected');
	});
});

http.listen(3000, function(){
  console.log('listening on *:3000');
});
