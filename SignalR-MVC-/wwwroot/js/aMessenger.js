var aMessengerConnection = new signalR.HubConnectionBuilder()
    .withUrl("/aMessengerHub")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

aMessengerConnection.on("ReciveUserConnection", function (userId, userName, isConnectedUser) {
    if (!isConnectedUser) {
        AddMessage(`User ${userName} is online`);
    }
});

function AddMessage(message) {
    if (message == null && message == '') {
        return;
    }
    let ui = document.getElementById('messagesList');
    let li = document.createElement('li');
    li.innerHTML = message;
    ui.appendChild(li);
}

aMessengerConnection.start();