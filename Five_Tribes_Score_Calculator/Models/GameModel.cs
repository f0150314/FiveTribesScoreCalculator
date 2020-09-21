using System;
using Five_Tribes_Score_Calculator.Helpers;

namespace Five_Tribes_Score_Calculator.Models
{
    public class GameModel
    {
        //Properties
        public int PlayerCount { get; set; }
        public GameTypes? GameType { get; set; } = null; //Set to null to prevent first enum type get selected by default 

        //Constructor
        public GameModel()
        {
        }
    }
}
