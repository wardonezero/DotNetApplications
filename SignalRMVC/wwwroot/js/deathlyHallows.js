var cloakCountSpan = document.getElementById("cloakCounter");
var stoneCountSpan = document.getElementById("stoneCounter");
var wandCountSpan = document.getElementById("wandCounter");

var deathlyHallowsConnection = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.None)
    .withUrl("/deathlyhallows").build();

deathlyHallowsConnection.on("UpdateDeathlyhallowsCount", (cloak, stone, wand) => {
    cloakCountSpan.innerText = cloak.toString();
    stoneCountSpan.innerText = stone.toString();
    wandCountSpan.innerText = wand.toString();
});

function fulfilled() {
    deathlyHallowsConnection.invoke("GetRaceStatus").then((raceCount) => {
        cloakCountSpan.innerText = raceCount.Cloak.toString();
        stoneCountSpan.innerText = raceCount.Stone.toString();
        wandCountSpan.innerText = raceCount.Wand.toString();
    });
}

function rejected() {

}

deathlyHallowsConnection.start().then(fulfilled, rejected);