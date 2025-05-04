var basicChatConnection = new signalR.HubConnectionBuilder()
    .withUrl("/basicChatHub").build();

document.getElementById("sendMessage").disabled = true;

basicChatConnection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} - ${message}`;
});

document.getElementById("sendMessage").addEventListener("click", function (event) {
    var sander = document.getElementById("senderEmail").value;
    var massage = document.getElementById("chatMessage").value;
    var reciver = document.getElementById("receiverEmail").value;

    if (reciver.length > 0) {
        basicChatConnection.send("SendMessageTo", sander, reciver, massage).catch(function (err) {
            console.error(err.toString());
        });
    }
    else {
        basicChatConnection.send("SendMessageToAll", sander, massage).catch(function (err) {
            console.error(err.toString());
        });
    }
    event.preventDefault();
});

basicChatConnection.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
});