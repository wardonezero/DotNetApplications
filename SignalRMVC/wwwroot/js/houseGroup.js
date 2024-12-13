var houseGroupConnection = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.None)
    .withUrl("/deathlyhallows").build();

houseGroupConnection.on("UpdateDeathlyhallowsCount", (cloak, stone, wand) => {
    cloakCountSpan.innerText = cloak.toString();
    stoneCountSpan.innerText = stone.toString();
    wandCountSpan.innerText = wand.toString();
});

function fulfilled() {
    houseGroupConnection.invoke("GetRaceStatus").then((raceCount) => {
        cloakCountSpan.innerText = raceCount.Cloak.toString();
        stoneCountSpan.innerText = raceCount.Stone.toString();
        wandCountSpan.innerText = raceCount.Wand.toString();
    });
}

function rejected() {

}

houseGroupConnection.start().then(fulfilled, rejected);