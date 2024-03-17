var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information) // None, Trace, Critical, Debug, Error, Warning 
    .withUrl("/hubs/userCount", signalR.HttpTransportType.Websockets).build(); // .ServerSentEvents or .LongPolling
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

function newWindowLoadedOnClient() {
    //connectionUserCount.send("NewWindowLoaded");//send doesn't return a value
    connectionUserCount.invoke("NewWindowLoaded", "Damian").then((value)=>console.log(value)); //invoke returns a value
}

function fulfilled() {
    console.log("Connection to User Hub Successful");
    newWindowLoadedOnClient();
}

function rejected() {
    console.log("Connection to User Hub Rejected");
}

connectionUserCount.start().then(fulfilled, rejected);