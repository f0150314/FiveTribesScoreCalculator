using System;
namespace Five_Tribes_Score_Calculator.Models
{
    public class ScoreModel : BaseModel
    {
        // Properties
        public int Coins { get; set; }
        
        public int Viziers { get; set; }

        public int Artisans { get; set; }

        public int Elders { get; set; }

        public int Djinns { get; set; }

        public int PalmTrees { get; set; }

        public int Palaces { get; set; }

        public int Camels { get; set; }

        public int PreciousItems { get; set; }

        public int Resources { get; set; }
    }
}
