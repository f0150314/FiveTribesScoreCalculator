using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Helpers;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Services
{
    public class PlayerServices : IPlayerServices
    {
        // Properties
        public List<PlayerModel> Players { get; set; }

        // Constructor
        public PlayerServices()
        {
            // Initialize player list
            Players = new List<PlayerModel>();
        }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns>a new ordered player list (a new instance)</returns>
        public List<PlayerModel> GetAllPlayers()
        {
            // Order by Name
            Players = Players.OrderBy(p => p.Name).ToList();

            // Set Id
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Id = i + 1;
            }
            
            return Players;
        }

        /// <summary>
        /// Add a new player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        public void AddPlayer(string name, string gender)
        {
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(gender))
            {
                // Create a new player
                PlayerModel player = new PlayerModel
                {
                    Name = name,
                    Gender = gender
                };

                Players.Add(player);

                // Notify a new player is added
                MessagingCenter.Send(this, Messages.AddPlayerMessage);
            }
        }

        /// <summary>
        /// Remove the specified player
        /// </summary>
        /// <param name="player"></param>
        public void RemovePlayer(object player)
        {
            if (player != null)
            { 
                Players.Remove((PlayerModel)player);

                // Notify the specified player is removed
                MessagingCenter.Send(this, Messages.RemovePlayerMessage);
            }
        }

        /// <summary>
        /// Find winners among players
        /// </summary>
        /// <returns></returns>
        public void FindWinners(ObservableCollection<PlayerModel> players)
        {
            // Reset winners (In case they change their scores)
            players.ToList().ForEach(p => p.IsWinner = false);

            // Get total scores
            List<int> totalScoreList = new List<int>();
            players.ToList().ForEach(p => totalScoreList.Add(p.TotalScore));

            // Set winners using total scores
            players.Where(p => p.TotalScore == totalScoreList.Max()).ToList().ForEach(p => p.IsWinner = true);
        }

        /// <summary>
        /// Update score of artisans and precious items to zero if base game is selected.
        /// </summary>
        public void UpdateScoreIfBaseGame(ObservableCollection<PlayerModel> players, GameModel game)
        {
            if (game.GameType.Equals(GameTypes.FT))
            {
                foreach (var player in players)
                {
                    player.Scores
                        .Where(s => s.Key.Equals("ARTISANS") || s.Key.Equals("ITEMS"))  // Find ARTISANS and ITEMS scores
                        .Select(s => s.Key).ToList()                                    // Get their corresponding key
                        .ForEach(k => player.Scores[k] = "0");                          // Update scores to 0
                }
            }
        }
    }
}
