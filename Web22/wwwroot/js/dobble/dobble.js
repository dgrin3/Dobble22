"use strict";

const PLAYER_STATUS = "Участник";
const SPECTATOR_STATUS = "Наблюдатель";
const GAME_PLAYING = "Играем";
const CHAT_LENGHT = 20;

var connection = new signalR.HubConnectionBuilder().withUrl("/dobbleHub").build();
document.getElementById("sendButton").disabled = true;

connection.on("SetGameStatus", function (status) {
    document.getElementById("gameStatus").value = status;
    SetElements();
});

connection.on("SetPlayerStatus", function (status) {
    document.getElementById("playerStatus").value = status;
    SetElements();
});

function SetElements() {
    var gameStatus = document.getElementById("gameStatus").value;
    var playerStatus = document.getElementById("playerStatus").value;
    document.getElementById("beginGameButton").disabled = gameStatus == GAME_PLAYING || playerStatus != PLAYER_STATUS;
    document.getElementById("endGameButton").disabled = gameStatus != GAME_PLAYING || playerStatus != PLAYER_STATUS;
    document.getElementById("joinGameButton").disabled = gameStatus == GAME_PLAYING || playerStatus == PLAYER_STATUS;
    document.getElementById("quitGameButton").disabled = playerStatus != PLAYER_STATUS;
    document.getElementById("playerName").disabled = playerStatus == PLAYER_STATUS;
    document.getElementById("gameCardRow").style.display = gameStatus == GAME_PLAYING ? "block" : "none";
    document.getElementById("playerCardRow").style.display = gameStatus == GAME_PLAYING && playerStatus == PLAYER_STATUS ? "block" : "none";
};

connection.on("SetPlayerList", function (players) {
    let i;
    let maxScore;
    let td0, td1;
    let table = document.getElementById("playerTable");
    let tableRows = table.getElementsByTagName('tr')
    while (tableRows.length > 1) {
        table.deleteRow(1);
    };
    if (players.length > 0) {
        maxScore = players[0].score;
        for (i = 0; i < players.length; i++) {
            let tr = table.insertRow();
            td0 = tr.insertCell(0);
            td0.innerHTML = players[i].name;
            td1 = tr.insertCell(1);
            td1.innerHTML = players[i].score.toString();
            if (maxScore > 0 && players[i].score == maxScore) {
                td0.classList.add("leader");
                td1.classList.add("leader");
            }
        }
    }
    else {
        let tr = table.insertRow();
        let td = tr.insertCell(0);
        td.innerHTML = "Пока никто не подключился";
        td.colSpan = 2;
    }
});

function SetCard(card, type) {
    var i;
    var imageString;
    for (i = 0; i < card.length; i++) {
        imageString = (i + 1).toString();
        document.getElementById("image" + type + imageString).src = "images/" + type + "/" + card[i].toString() + ".png";
    }
}

connection.on("SetGameCard", function (card) {
    SetCard(card, "Game");
    var playerName = document.getElementById("playerName").value;
    connection.invoke("GetPlayerCard", playerName).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("SetPlayerCard", function (card) {
    SetCard(card, "Player");
});


connection.on("ReceiveMessage", function (message) {
    let table = document.getElementById("chatTable");
    let tableRows = table.getElementsByTagName('tr')
    while (tableRows.length >= CHAT_LENGHT) {
        table.deleteRow(0);
    };
    let tr = table.insertRow();
    tr.insertCell(0).innerHTML = message;
});

connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        document.getElementById("playerStatus").value = SPECTATOR_STATUS;
        connection.invoke("GetConnected")
    }).catch(function (err) {
        return console.error(err.toString());
});

// Добавление обработчика событий для события click кнопки sendButton
// При срабатывании этого события вызывается функция, которая через connection.invoke 
// вызывает метод SendMessage сервера с параметрами, равными значениям playerName и messageInput

// This adds an event listener for the click event to the sendButton button.
// When the event is fired the function is called that uses connection.invoke to call the server
// method SendMessage with parameters equal to values of playerName and messageInput textboxes
document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("playerName").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("beginGameButton").addEventListener("click", function (event) {
    BeginGame();
});

function BeginGame() {
    var player = document.getElementById("playerName").value;
    connection.invoke("BeginGame", player).catch(function (err) {
        return console.error(err.toString());
    });
};

document.getElementById("endGameButton").addEventListener("click", function () {
    EndGame();
});

function EndGame() {
    var player = document.getElementById("playerName").value;
    connection.invoke("EndGame", player).catch(function (err) {
        return console.error(err.toString());
    });
};

document.getElementById("joinGameButton").addEventListener("click", function (event) {
    var player = document.getElementById("playerName").value;
    connection.invoke("JoinGame", player).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("quitGameButton").addEventListener("click", function (event) {
    var player = document.getElementById("playerName").value;
        connection.invoke("QuitGame", player).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function MakeMove(source) {
    var imageName = document.getElementById(source).src.toString();
    var position = imageName.lastIndexOf("/") + 1;
    imageName = imageName.substr(position, imageName.length - position);
    position = imageName.lastIndexOf(".");
    imageName = imageName.substr(0, position);
    var picture = parseInt(imageName);
    var player = document.getElementById("playerName").value;
    connection.invoke("MakeMove", player, picture).catch(function (err) {
        return console.error(err.toString());
    });
}

document.getElementById("imageGame1").addEventListener("click", function (event) {
    MakeMove("imageGame1");
    event.preventDefault();
});

document.getElementById("imagePlayer1").addEventListener("click", function (event) {
    MakeMove("imagePlayer1");
    event.preventDefault();
});

document.getElementById("imageGame2").addEventListener("click", function (event) {
    MakeMove("imageGame2");
    event.preventDefault();
});

document.getElementById("imagePlayer2").addEventListener("click", function (event) {
    MakeMove("imagePlayer2");
    event.preventDefault();
});

document.getElementById("imageGame3").addEventListener("click", function (event) {
    MakeMove("imageGame3");
    event.preventDefault();
});

document.getElementById("imagePlayer3").addEventListener("click", function (event) {
    MakeMove("imagePlayer3");
    event.preventDefault();
});

document.getElementById("imageGame4").addEventListener("click", function (event) {
    MakeMove("imageGame4");
    event.preventDefault();
});

document.getElementById("imagePlayer4").addEventListener("click", function (event) {
    MakeMove("imagePlayer4");
    event.preventDefault();
});

document.getElementById("imageGame5").addEventListener("click", function (event) {
    MakeMove("imageGame5");
    event.preventDefault();
});

document.getElementById("imagePlayer5").addEventListener("click", function (event) {
    MakeMove("imagePlayer5");
    event.preventDefault();
});

document.getElementById("imageGame6").addEventListener("click", function (event) {
    MakeMove("imageGame6");
    event.preventDefault();
});

document.getElementById("imagePlayer6").addEventListener("click", function (event) {
    MakeMove("imagePlayer6");
    event.preventDefault();
});

document.getElementById("imageGame7").addEventListener("click", function (event) {
    MakeMove("imageGame7");
    event.preventDefault();
});

document.getElementById("imagePlayer7").addEventListener("click", function (event) {
    MakeMove("imagePlayer7");
    event.preventDefault();
});

document.getElementById("imageGame8").addEventListener("click", function (event) {
    MakeMove("imageGame8");
    event.preventDefault();
});

document.getElementById("imagePlayer8").addEventListener("click", function (event) {
    MakeMove("imagePlayer8");
    event.preventDefault();
});
