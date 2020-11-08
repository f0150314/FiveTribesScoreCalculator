using System;
using Five_Tribes_Score_Calculator.ViewModels;

namespace Five_Tribes_Score_Calculator.Helpers
{
    public static class ViewModelLocator
    {
        // Public static properties
        public static MainPageViewModel MainPageViewModel { get; }
            = new MainPageViewModel(App.NavigationServices, App.DialogServices);

        public static PlayerConfigViewModel PlayerConfigViewModel { get; }
            = new PlayerConfigViewModel(App.NavigationServices, App.DialogServices, App.PlayerServices);

        public static EditScoresViewModel EditScoresViewModel { get; }
            = new EditScoresViewModel(App.NavigationServices, App.PlayerServices);

        public static ScoreSheetViewModel ScoreSheetViewModel { get; }
            = new ScoreSheetViewModel(App.NavigationServices);
    }
}
