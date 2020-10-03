using System;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Models;
using System.Linq;

namespace Five_Tribes_Score_Calculator.Services
{
    public class PlayerServices : IPlayerServices
    {
        private PlayerModel player = null;

        // Properties
        private List<PlayerModel> players;
        public List<PlayerModel> Players
        {
            get => players.OrderBy(p => p.Name).ToList();
            set
            {
                players = value;
            }
        }

        // Constructor
        public PlayerServices()
        {
            Players = new List<PlayerModel>();
        }

        /// <summary>
        /// Get all players
        /// </summary>
        /// <returns>player list</returns>
        public List<PlayerModel> GetAllPlayers() => Players;

        /// <summary>
        /// Add a new player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        public void AddPlayer(string name, string gender)
        {
            // Create a new player
            player = new PlayerModel
            {
                Name = name,
                Gender = gender
            };

            Players.Add(player);
        }

        /// <summary>
        /// Remove the specified player
        /// </summary>
        /// <param name="player"></param>
        public void DeletePlayer(object player)
        {
            Players.Remove((PlayerModel)player);
        }
    }
}
