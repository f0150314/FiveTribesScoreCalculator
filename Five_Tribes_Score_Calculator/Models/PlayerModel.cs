using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Models
{
    public class PlayerModel : BaseModel
    {
        // Private fields
        private string gender;
        private int totalScore = 0;
        private Dictionary<string, string> scores;
        private bool markAsComplete = false;

        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Color GenderColor { get; set; }
        public bool IsWinner { get; set; } = false;

        public bool MarkAsComplete
        {
            get => markAsComplete;
            set
            {
                markAsComplete = value;
                OnPropertyChanged(nameof(MarkAsComplete));
            }
        } 

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

        public int TotalScore
        {
            get
            {
                if (Scores != null && Scores.Count > 0)
                {
                    // Add scores if it is not an empty string.
                    return Scores.Where(s => !string.IsNullOrWhiteSpace(s.Value)).Sum(s => Convert.ToInt32(s.Value));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                totalScore = value;
                OnPropertyChanged(nameof(TotalScore));
            }
        }

        public Dictionary<string, string> Scores
        {
            get => scores;
            set
            {
                scores = value;
                OnPropertyChanged(nameof(Scores));
            }
        }
    }
}
