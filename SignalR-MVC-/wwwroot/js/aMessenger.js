var aMessengerConnection = new signalR.HubConnectionBuilder()
    .withUrl("/aMessengerHub").build();
aMessengerConnection.start();