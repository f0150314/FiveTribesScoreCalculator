using System;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Models
{
    public class PlayerModel
    {
        // Properties
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public ScoreModel Score { get; set; }

        public int TotalScore { get; set; }

        public Color GenderColor { get; set; }
    }
}
