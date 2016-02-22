var io = require('socket.io')({
    transports: ['websocket'],
});

io.attach(4567);

io.on('connection', function (socket) {
    console.log("connected");

    // unity2node
    socket.on('unity2node', function (data) {
        console.log("unity2node: " + JSON.stringify(data));
    });

    // node2unity
    setInterval(function () {
        socket.emit('node2unity', {data1: "Fabi", data2: "Frech"});
    }, 3000);
})
