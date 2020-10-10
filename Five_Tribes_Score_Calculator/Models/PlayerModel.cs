using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Models
{
    public class PlayerModel : BaseModel
    {
        // Private fields
        private string gender;

        // Properties
        public int Id { get; set; }

        public string Gender
        {
            get => gender;
            set
            {
                gender = value;

                // Set Color
                GenderColor = gender == "Male" ? Color.DodgerBlue : Color.Red;
            }
        }

        public string Name { get; set; }

        public int TotalScore { get; set; } = 0;

        public Color GenderColor { get; set; }

        public bool IsWinner { get; set; } = false;

        public bool MakrAsComplete { get; set; } = true;
    }
}
