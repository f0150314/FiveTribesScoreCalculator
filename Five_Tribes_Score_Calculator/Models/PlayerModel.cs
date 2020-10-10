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
        public bool MakrAsComplete { get; set; } = false;

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
                { "ScoreCoins", 0 },
                { "ScoreViziers", 0 },
                { "ScoreArtisans", 0 },
                { "ScoreElders", 0 },
                { "ScoreDjinns", 0 },
                { "ScorePalmTrees", 0 },
                { "ScorePalaces", 0 },
                { "ScoreCamels", 0 },
                { "ScorePreciousItems", 0 },
                { "ScoreResources", 0 }
            };
        }
    }
}
