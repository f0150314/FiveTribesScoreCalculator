using System;
namespace Five_Tribes_Score_Calculator.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }
        public ScoreModel Score { get; set; }
        public int TotalScore { get; set; }

        public PlayerModel()
        {
        }
    }
}
