var aMessengerConnection = new signalR.HubConnectionBuilder()
    .withUrl("/aMessengerHub")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

aMessengerConnection.on("ReciveUserConnection", function (userId, userName) {
    AddMessage(`User ${userName} Connected `);
});

aMessengerConnection.on("ReciveUserDisconnection", function (userId, userName) {
    AddMessage(`User ${userName} Disconnected`);
});

aMessengerConnection.on("ReciveAddPrivateChat", function (maxChats, privateChatId, privateChatName, userId, userName) {
    AddMessage(`User ${userName} created private chat ${privateChatName}`);
    FillRoomDropDown();
});

aMessengerConnection.on("ReciveDeletePrivateChat", function (deleteddeleted, selected, privateChatName, userName) {
    AddMessage(`User ${userName} deleted private chat ${privateChatName}`);
    FillRoomDropDown();
});

aMessengerConnection.on("RecivePublicMessage", function (chatId, userId, userName, message, chatName) {
    AddMessage(`[Public message ${chatName}] ${userName} - ${message}`);
});

aMessengerConnection.on("RecivePrivateMessage", function (senderId, senderName, reciverId, message, chatId, reciverName) {
    AddMessage(`[Private message to ${reciverName}] ${senderName} - ${message}`);
});

function sendPublicMessage() {
    let inputmessage = document.getElementById('txtPublicMessage');
    let ddlSelRoom = document.getElementById('ddlSelRoom');
    let chatId = ddlSelRoom.value;
    let chatName = ddlSelRoom.options[ddlSelRoom.selectedIndex].text;
    var message = inputmessage.value;
    if (!message) {
        return;
    }
    aMessengerConnection.send("SendPublicMessage", Number(chatId), message, chatName).then(() => {
        console.log("Message sent successfully.");
    })
    .catch(err => {
        console.error("Error sending message:", err);
    });
    inputmessage.value = '';
}

function sendPrivateMessage() {
    let inputmessage = document.getElementById('txtPrivateMessage');
    let ddlSelUser = document.getElementById('ddlSelUser');
    let reciverId = ddlSelUser.value;
    let reciverName = ddlSelUser.options[ddlSelUser.selectedIndex].text;
    var message = inputmessage.value;
    if (!message) {
        return;
    }
    aMessengerConnection.send("SendPrivateMessage", reciverId, message, reciverName).then(() => {
        console.log("Message sent successfully.");
    })
        .catch(err => {
            console.error("Error sending message:", err);
        });
    inputmessage.value = '';
}

function AddNewRoom(maxChats) {
    let createRoomName = document.getElementById('createRoomName');
    var roomName = createRoomName.value;
    if (!roomName) {
        return;
    }
    /*POST*/
    $.ajax({
        url: '/PrivateChats/PostPrivateChat',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ ChatId: 0, ChatName: roomName }),
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*ADD ROOM COMPLETED SUCCESSFULLY*/
            aMessengerConnection.send("AddPrivateChat", maxChats, json.chatId, json.chatName);
            createRoomName.value = '';
        },
        error: function (xhr) {
            console.error('Response:', xhr.responseText);
            alert('error in AddNewRoom');
        }
    })
}

function DeleteRoom() {
    let deleteRoom = document.getElementById('ddlDelRoom');
    var roomName = deleteRoom.options[deleteRoom.selectedIndex].text;
    let text = `Are you sure you want to delete ${roomName} chat ?`;
    if (confirm(text) == false) {
        return;
    }
    if (!roomName) {
        return;
    }
    let chatId = deleteRoom.value;
    $.ajax({
        url: `/PrivateChats/DeletePrivateChat/${chatId}`,
        dataType: "json",
        type: "DELETE",
        contentType: 'application/json; charset=utf-8',
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*ADD ROOM COMPLETED SUCCESSFULLY*/
            aMessengerConnection.send("DeletePrivateChat", json.deleted, json.selected, roomName);
            FillRoomDropDown();
        },
        error: function (xhr) {
            console.error('Response:', xhr.responseText);
            alert('error in AddNewRoom');
        }
    })
}

document.addEventListener('DOMContentLoaded', (event) => {
    FillRoomDropDown();
    FillUserDropDown();
})

function FillUserDropDown() {
    $.getJSON('/PrivateChats/GetPrivateChatUser')
        .done(function (json) {
            var ddlSelUser = document.getElementById("ddlSelUser");
            ddlSelUser.innerText = null;
            json.forEach(function (item) {
                var newOption = document.createElement("option");
                newOption.text = item.userName;//item.whateverProperty
                newOption.value = item.id;
                ddlSelUser.add(newOption);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });
}

function FillRoomDropDown() {
    $.getJSON('/PrivateChats/GetPrivateChats')
        .done(function (json) {
            var ddlDelRoom = document.getElementById("ddlDelRoom");
            var ddlSelRoom = document.getElementById("ddlSelRoom");
            ddlDelRoom.innerText = null;
            ddlSelRoom.innerText = null;
            json.forEach(function (item) {
                var newOption = document.createElement("option");
                newOption.text = item.chatName;
                newOption.value = item.chatId;
                ddlDelRoom.add(newOption);
                var newOption1 = document.createElement("option");
                newOption1.text = item.chatName;
                newOption1.value = item.chatId;
                ddlSelRoom.add(newOption1);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });
}

function AddMessage(message) {
    if (message == null && message == '') {
        return;
    }
    let ui = document.getElementById('messagesList');
    let li = document.createElement('li');
    li.innerHTML = message;
    ui.appendChild(li);
}

aMessengerConnection.start().then(function () {
    console.log("SignalR connection established.");
}).catch(function (err) {
    return console.error(err.toString());
});;