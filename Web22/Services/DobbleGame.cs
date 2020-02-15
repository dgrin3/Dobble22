using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Web22.Constants;
using Web22.Enums;
using Web22.Helper;
using Web22.Models;

namespace Web22.Services
{
    public class DobbleGame : IDobbleGame
    {
        private object lockObject = new object();

        private List<DobbleCard> pack;
        private int currentCardNumber = 0;
        private List<DobblePlayer> players = new List<DobblePlayer>();
        public DobbleCard CurrentCard
        {
            get => pack.Skip(currentCardNumber).First();
        }
        public string Status { get; private set; }
        public string StatusName => Status.ToString();
        public List<DobblePlayerInfo> Players 
        {
            get => players
                    .Select(p => new DobblePlayerInfo(p.Name, p.Score))
                    .OrderByDescending(p => p.Score)
                    .ThenBy(p => p.Name)
                    .ToList();
        }
        public DobbleGame()
        {
            EndGame();
        }
        public DobbleCard PlayerCard(string playerName)
        {
            return GetPlayerByName(playerName).Card;
        }

        private DobblePlayer GetPlayerByName(string playerName)
        {
            return players.FirstOrDefault(p => p.Name == playerName);
        }

        private RequestResult LockGame()
        {
            var result = new RequestResult();
            if (Monitor.TryEnter(lockObject, TimeSpan.FromSeconds(DobbleConstants.LockAttemptSeconds)))
            {
                result.Status = ResultStatus.Success;
                result.Message = "Lock acquired";
            }
            else
            {
                result.Status = ResultStatus.Failure;
                result.Message = "Unable to acquire a lock";
            }
            return result;
        }

        private void UnlockGame()
        {
            if (Monitor.IsEntered(lockObject))
            {
                Monitor.Exit(lockObject);
            }
            return;
        }

        public RequestResult BeginGame()
        {
            var result = LockGame();
            if (result.Status == ResultStatus.Success)
            {
                GetPack();
                DealAllPlayers();
                Status = DobbleConstants.GamePlayingStatusName;

                UnlockGame();
            }
            return result;
        }

        public RequestResult EndGame()
        {
            var result = LockGame();
            if (result.Status == ResultStatus.Success)
            {
                currentCardNumber = 0;
                foreach (var p in players)
                {
                    p.Card = null;
                    p.Score = 0;
                }
                Status = DobbleConstants.GameJoiningStatusName;

                UnlockGame();
            }
            return result;
        }

        public RequestResult JoinGame(string playerName)
        {
            var result = LockGame();
            if (result.Status == ResultStatus.Success)
            {
                if (Status != DobbleConstants.GameJoiningStatusName)
                {
                    result.Status = ResultStatus.Failure;
                    result.Message = "Подключение к игре сейчас невозможно";
                }
                else if (string.IsNullOrEmpty(playerName))
                {
                    result.Status = ResultStatus.Failure;
                    result.Message = "Нужно указать имя участника";
                }
                else if (players.Any(p => p.Name == playerName))
                {
                    result.Status = ResultStatus.Failure;
                    result.Message = "Участник с таким именем уже в игре. Нужно выбрать другое имя.";
                }
                else
                {
                    players.Add(new DobblePlayer() { Name = playerName, Score = 0 });
                    result.Status = ResultStatus.Success;
                    result.Message = "Успешное подключение";
                }

                UnlockGame();
            }
            return result;
        }

        public RequestResult QuitGame(string playerName)
        {
            var result = LockGame();
            if (result.Status == ResultStatus.Success)
            {
                if (string.IsNullOrEmpty(playerName))
                {
                    result.Status = ResultStatus.Failure;
                    result.Message = "Нужно указать имя участника";
                }
                else if (!players.Any(p => p.Name == playerName))
                {
                    result.Status = ResultStatus.Failure;
                    result.Message = "Участник с указанным именем не найден";
                }
                else
                {
                    players.Remove(players.First(p => p.Name == playerName));
                    result.Status = ResultStatus.Success;
                    result.Message = "Успешное отключение от игры";
                }

                UnlockGame();
            }
            return result;
        }
        public RequestResult MakeMove(string playerName, int picture)
        {
            var result = LockGame();
            if (result.Status == ResultStatus.Success)
            {
                try
                {
                    var player = GetPlayerByName(playerName);
                    if (player == null)
                    {
                        result.Status = ResultStatus.Failure;
                        result.Message = $"{playerName} не участвует в игре";
                    }
                    else if (Status != DobbleConstants.GamePlayingStatusName)
                    {
                        result.Status = ResultStatus.Failure;
                        result.Message = "Игра ещё не началась";
                    }
                    else if (player.Card.HavePicture(picture) && CurrentCard.HavePicture(picture))
                    {
                        player.Score++;
                        if (currentCardNumber < DobbleConstants.NumberOfCards - 1)
                        {
                            DealPlayer(playerName);
                        }
                        else
                        {
                            Status = DobbleConstants.GameJoiningStatusName;
                        }
                        result.Status = ResultStatus.Success;
                        result.Message = "Правильный ответ";
                    }
                    else
                    {
                        result.Status = ResultStatus.Failure;
                        result.Message = "Неправильный ответ";
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    UnlockGame();
                }
            }

            return result;
        }
        private void GetPack()
        {
            pack = DobblePackHelper.ShuffleDobblePack();
        }

        private void DealPlayer(string playerName)
        {
            GetPlayerByName(playerName).Card=CurrentCard;
            currentCardNumber++;
        }
        private void DealAllPlayers()
        {
            foreach(var p in players)
            {
                DealPlayer(p.Name);
            }
        }

        private void CompilePack()
        {
            int[] pictures = new int[DobbleConstants.NumberOfPictures];
            pack.RemoveAll(p => true);

            for (int i = 1; i <= DobbleConstants.NumberOfCards; i++)
            {
                DobbleCard card = new DobbleCard();
                for (int j = 0; j < DobbleConstants.NumberOfPictures
                                    && card.Counter <= DobbleConstants.GameDimension;)
                {
                    int picture = j + 1;
                    if (pictures[j] <= DobbleConstants.GameDimension
                        && !(pack.Any(c => c.HaveSamePicture(card)
                                            && c.HavePicture(picture))))
                    {
                        card.AddPicture(picture);
                        pictures[j]++;
                    }
                    j++;
                    while (j == DobbleConstants.NumberOfPictures
                            && card.Counter != DobbleConstants.GameDimension + 1)
                    {
                        j = card.Pictures[card.Counter - 1];
                        pictures[j - 1]--;
                        card.RemovePicture();
                    }
                }
                pack.Add(card);
            }
        }
    }
}
