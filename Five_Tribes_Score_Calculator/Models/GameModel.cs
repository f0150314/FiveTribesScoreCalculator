using System;
using Five_Tribes_Score_Calculator.Helpers;

namespace Five_Tribes_Score_Calculator.Models
{
    public class GameModel
    {
        // Properties
        public int PlayerCount { get; set; } = 0;

        public GameTypes? GameType { get; set; } = null; // Null by default because of ENUM
    }
}
