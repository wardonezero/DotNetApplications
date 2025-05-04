document.getElementById("sendButton").disabled = true;

var notificationsConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationsHub").build();

notificationsConnection.on("LoadNotification", function (notifications, count) {
    document.getElementById("notificationsList").innerHTML = "";
    var notificationsCount = document.getElementById("notificationsCount");
    notificationsCount.innerHTML = "<span>(" + count + ")</span>";
    for (let i = notifications.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = notifications[i];
        document.getElementById("notificationsList").appendChild(li);
    }
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    notificationsConnection.send("SendNotification", message).then(function () {
        document.getElementById("notificationInput").value = "";
    });
    event.preventDefault();
});

notificationsConnection.start().then(function () {
    notificationsConnection.send("GetNotifications");
    document.getElementById("sendButton").disabled = false;
});