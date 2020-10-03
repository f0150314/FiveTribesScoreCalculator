using System;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Models;

namespace Five_Tribes_Score_Calculator.Services
{
    public interface IPlayerServices
    {
        List<PlayerModel> GetAllPlayers();

        void AddPlayer(string name, string gender);

        void DeletePlayer(object player);
    }
}
