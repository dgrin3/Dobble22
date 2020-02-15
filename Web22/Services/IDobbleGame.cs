using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web22.Enums;
using Web22.Models;

namespace Web22.Services
{
    public interface IDobbleGame
    {
        string Status { get; }
        string StatusName { get; }

        DobbleCard CurrentCard { get; }

        List<DobblePlayerInfo> Players { get; }

        DobbleCard PlayerCard(string playerName);
        RequestResult BeginGame();
        RequestResult EndGame();
        RequestResult JoinGame(string playerName);
        RequestResult QuitGame(string playerName);
        RequestResult MakeMove(string playerName, int picture);
    }
}
