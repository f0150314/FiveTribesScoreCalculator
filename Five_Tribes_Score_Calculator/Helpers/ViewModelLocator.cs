using System;
using Five_Tribes_Score_Calculator.ViewModels;

namespace Five_Tribes_Score_Calculator.Helpers
{
    public static class ViewModelLocator
    {
        // Public static properties
        public static MainPageViewModel MainPageViewModel { get; }
            = new MainPageViewModel();

        public static ScoreSheetViewModel ScoreSheetViewModel { get; }
            = new ScoreSheetViewModel();

        public static PlayerConfigViewModel PlayerConfigViewModel { get; }
            = new PlayerConfigViewModel();
    }
}
