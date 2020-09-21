using System;
namespace Five_Tribes_Score_Calculator.Models
{
    public class GameModel
    {
        public enum GameType
        {
            FT,
            AQ,
            WS
        }

        public int PlayerCount { get; set; }

        public GameModel()
        {
        }
    }
}
