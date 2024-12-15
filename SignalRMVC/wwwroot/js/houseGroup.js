let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");

var houseGroupConnection = new signalR.HubConnectionBuilder()
    .withUrl("/housegrouphub").build();

btn_gryffindor.addEventListener("click", function (event) {
    houseGroupConnection.send("SubscribeToHouse", "Gryffindor");
    event.preventDefault();
});
btn_slytherin.addEventListener("click", function (event) {
    houseGroupConnection.send("SubscribeToHouse", "Slytherin");
    event.preventDefault();
});
btn_hufflepuff.addEventListener("click", function (event) {
    houseGroupConnection.send("SubscribeToHouse", "Hufflepuff");
    event.preventDefault();
});
btn_ravenclaw.addEventListener("click", function (event) {
    houseGroupConnection.send("SubscribeToHouse", "Ravenclaw");
    event.preventDefault();
});

btn_un_gryffindor.addEventListener("click", function (event) {
    houseGroupConnection.send("UnsubscribeFromHouse", "Gryffindor");
    event.preventDefault();
});
btn_un_slytherin.addEventListener("click", function (event) {
    houseGroupConnection.send("UnsubscribeFromHouse", "Slytherin");
    event.preventDefault();
});
btn_un_hufflepuff.addEventListener("click", function (event) {
    houseGroupConnection.send("UnsubscribeFromHouse", "Hufflepuff");
    event.preventDefault();
});
btn_un_ravenclaw.addEventListener("click", function (event) {
    houseGroupConnection.send("UnsubscribeFromHouse", "Ravenclaw");
    event.preventDefault();
});

houseGroupConnection.on("MemberAddedToHouse", (houseName) => {
    toastr.warning(`Member has subscribed to the ${houseName}`)
})

houseGroupConnection.on("MemberRemovedFromHouse", (houseName) => {
    toastr.warning(`Member has unsubscribed from the ${houseName}`)
})

houseGroupConnection.on("SubscriptionStatus", (houseJoinedStatus, houseName, isSubscribed) => {
    lbl_houseJoined.innerText = houseJoinedStatus;
    if (isSubscribed) {
        switch (houseName) {
            case "Gryffindor":
                btn_gryffindor.style.display = "none";
                btn_un_gryffindor.style.display = "";
                break;
            case "Slytherin":
                btn_slytherin.style.display = "none";
                btn_un_slytherin.style.display = "";
                break;
            case "Hufflepuff":
                btn_hufflepuff.style.display = "none";
                btn_un_hufflepuff.style.display = "";
                break;
            case "Ravenclaw":
                btn_ravenclaw.style.display = "none";
                btn_un_ravenclaw.style.display = "";
                break;
            default:
                break;
        }
        toastr.success(`You have subscribed successfully. ${houseName}`)
    }
    else {
        switch (houseName) {
            case "Gryffindor":
                btn_gryffindor.style.display = "";
                btn_un_gryffindor.style.display = "none";
                break;
            case "Slytherin":
                btn_slytherin.style.display = "";
                btn_un_slytherin.style.display = "none";
                break;
            case "Hufflepuff":
                btn_hufflepuff.style.display = "";
                btn_un_hufflepuff.style.display = "none";
                break;
            case "Ravenclaw":
                btn_ravenclaw.style.display = "";
                btn_un_ravenclaw.style.display = "none";
                break;
            default:
                break;
        }
        toastr.success(`You have unsubscribed successfully. ${houseName}`)

    }
});

function fulfilled() {

}

function rejected() {

}

houseGroupConnection.start().then(fulfilled, rejected);