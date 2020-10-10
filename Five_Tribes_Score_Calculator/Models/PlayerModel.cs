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
        private Dictionary<string, int> scoreDic;

        // Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public Color GenderColor { get; set; }
        public bool IsWinner { get; set; } = false;
        public bool MakrAsComplete { get; set; } = true;

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
                if (scoreDic != null && scoreDic.Count > 0)
                {
                    return scoreDic.Sum(s => s.Value);
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

        public Dictionary<string, int> ScoreDic
        {
            get => scoreDic;
            set
            {
                scoreDic = value;
                OnPropertyChanged(nameof(ScoreDic));
            }
        }

        // Constructor
        public PlayerModel()
        {
            // Initialize score dictionary
            scoreDic = new Dictionary<string, int>()
            {
                { "ScoreCoins", 8 },
                { "ScoreViziers", 50 },
                { "ScoreArtisans", 0 },
                { "ScoreElders", 220 },
                { "ScoreDjinns", 10 },
                { "ScorePalmTrees", 73 },
                { "ScorePalaces", 38 },
                { "ScoreCamels", 78 },
                { "ScorePreciousItems", 20 },
                { "ScoreResources", 10 }
            };
        }
    }
}
