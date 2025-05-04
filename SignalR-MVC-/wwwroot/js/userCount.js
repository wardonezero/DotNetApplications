var connection = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.None)
    .withUrl("/usercountHub").build();

connection.on("UpdateTotalViewers", (value) => {
    var newCountSpan = document.getElementById("TotalViewersCounter");
    newCountSpan.innerText = value.toString();
});

connection.on("UpdateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("TotalUsersCounter");
    newCountSpan.innerText = value.toString();
});

function newViewer() {
    connection.send("NewViewer");
}

function fulfilled() {
    newViewer();
}

connection.start().then(fulfilled);