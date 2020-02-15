using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web22.Constants;
using Web22.Enums;
using Web22.Services;

namespace DobbleWeb.Hubs
{
    public class DobbleHub : Hub
    {
        private readonly IDobbleGame _dobbleGame;

        public DobbleHub(IDobbleGame dobbleGame)
        {
            _dobbleGame = dobbleGame;
        }
        public async Task GetConnected()
        {
            await Clients.Caller.SendAsync("SetGameStatus", _dobbleGame.StatusName);
            await Clients.Caller.SendAsync("SetPlayerList", _dobbleGame.Players);
            await Clients.Caller.SendAsync("ReceiveMessage", "Добро пожаловать!");
            await Clients.Others.SendAsync("ReceiveMessage", "Подключился новый гость");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var playerName = Context.Items["PlayerName"]?.ToString();
            if (!string.IsNullOrEmpty(playerName))
            {
                var result = _dobbleGame.QuitGame(playerName);
                if (result.Status == ResultStatus.Success)
                {
                    await Clients.Others.SendAsync("SetPlayerList", _dobbleGame.Players);
                    await Clients.Others.SendAsync("ReceiveMessage", $"{playerName} вышел из игры");
                };
                await Clients.Others.SendAsync("ReceiveMessage", $"{playerName} отключился");
            }
            else
            {
                await Clients.Others.SendAsync("ReceiveMessage", $"Один из гостей отключился");
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task BeginGame(string playerName)
        {
            var result = _dobbleGame.BeginGame();
            if (result.Status == ResultStatus.Success)
            {
                await Clients.All.SendAsync("SetGameStatus", _dobbleGame.StatusName);
                await Clients.All.SendAsync("SetGameCard", _dobbleGame.CurrentCard.Pictures);
                await Clients.All.SendAsync("SetPlayerList", _dobbleGame.Players);
                await Clients.All.SendAsync("ReceiveMessage", $"Игра началась. Старт дан участником: {playerName}");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"Ошибка: {result.Message}");
            }
        }
        public async Task EndGame(string playerName)
        {
            var result = _dobbleGame.EndGame();
            if (result.Status == ResultStatus.Success)
            {
                await Clients.All.SendAsync("SetGameStatus", _dobbleGame.StatusName);
                await Clients.All.SendAsync("ReceiveMessage", $"Игра окончена. Завершил игру участник: {playerName}");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"Ошибка: {result.Message}");
            }
        }
        public async Task JoinGame(string playerName)
        {
            var result = _dobbleGame.JoinGame(playerName);
            if (result.Status == ResultStatus.Success)
            {
                Context.Items.Add("PlayerName", playerName);
                await Clients.Caller.SendAsync("SetPlayerStatus", DobbleConstants.PlayerStatusName);
                await Clients.All.SendAsync("SetPlayerList", _dobbleGame.Players);
                await Clients.Others.SendAsync("ReceiveMessage", $"К игре присоединился участник: {playerName}");
                await Clients.Caller.SendAsync("ReceiveMessage", $"{playerName}, добро пожаловать в игру!");
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"Ошибка: {result.Message}");
            }
        }
        public async Task QuitGame(string playerName)
        {
            var result = _dobbleGame.QuitGame(playerName);
            if (result.Status == ResultStatus.Success)
            {
                Context.Items.Remove("PlayerName");
                await Clients.Caller.SendAsync("SetPlayerStatus", DobbleConstants.SpectatorStatusName);
                await Clients.All.SendAsync("SetPlayerList", _dobbleGame.Players);
                await Clients.Others.SendAsync("ReceiveMessage", $"Участник {playerName} вышел из игры");
                await Clients.Caller.SendAsync("ReceiveMessage", $"{playerName}, до свидания!");
                if (!(_dobbleGame.Players.Any()) && _dobbleGame.Status == DobbleConstants.GamePlayingStatusName)
                {
                    await EndGame($"{playerName} (последний участник вышел из игры)");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", $"Ошибка: {result.Message}");
            }
        }
        public async Task MakeMove(string playerName, int picture)
        {
            var result = _dobbleGame.MakeMove(playerName, picture);
            if (result.Status == ResultStatus.Success)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", result.Message);
                await Clients.Others.SendAsync("ReceiveMessage", $"Участник {playerName} дал правильный ответ");
                await Clients.All.SendAsync("SetPlayerList", _dobbleGame.Players);
                if (_dobbleGame.Status == DobbleConstants.GamePlayingStatusName)
                {
                    await Clients.All.SendAsync("SetGameCard", _dobbleGame.CurrentCard.Pictures);
                    await Clients.Caller.SendAsync("SetPlayerCard", _dobbleGame.PlayerCard(playerName).Pictures);
                }
                else
                {
                    await EndGame($"{playerName} (дал заключительный ответ)");
                }
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessage", "Ошибка: " + result.Message);
            }
        }
        public async Task GetPlayerCard(string playerName)
        {
            await Clients.Caller.SendAsync("SetPlayerCard", _dobbleGame.PlayerCard(playerName).Pictures);
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", $"{user}: {message}");
            await Clients.Caller.SendAsync("ReceiveMessage", $"Вы ({user}): {message}");
        }
    }
}
