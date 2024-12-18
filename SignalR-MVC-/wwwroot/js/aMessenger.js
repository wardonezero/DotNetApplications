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

function AddNewRoom(maxChats) {
    let createRoomName = document.getElementById('createRoomName');
    var roomName = createRoomName.value;
    if (roomName == null && roomName == '') {
        return;
    }
    /*POST*/
    $.ajax({
        url: '/PrivateChats/PostPrivateChat',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: 0, name: roomName }),
        async: true,
        processData: false,
        cache: false,
        success: function (json) {
            /*ADD ROOM COMPLETED SUCCESSFULLY*/
            aMessengerConnection.send("AddPrivateChat", maxChats, json.id, json.name);
            createRoomName.value = '';
        },
        error: function (xhr) {
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
                newOption.text = item.name;
                newOption.value = item.id;
                ddlDelRoom.add(newOption);
                var newOption1 = document.createElement("option");
                newOption1.text = item.name;
                newOption1.value = item.id;
                ddlSelRoom.add(newOption1);
            });
        })
        .fail(function (jqxhr, textStatus, error) {
            var err = textStatus + ", " + error;
            console.log("Request Failed: " + jqxhr.detail);
        });
}

aMessengerConnection.start();