var notificationsConnection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationsHub").build();

notificationsConnection.on("ReceiveNotification", function (message) => {
    
});







notificationsConnection.start();