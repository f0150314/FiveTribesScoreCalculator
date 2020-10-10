using System;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Helpers;
using System.Linq;
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
    }
}
