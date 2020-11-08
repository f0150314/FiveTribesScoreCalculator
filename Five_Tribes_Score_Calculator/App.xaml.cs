using System;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Services;
using Five_Tribes_Score_Calculator.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Five_Tribes_Score_Calculator
{
    public partial class App : Application
    {
        // Properties
        public static INavigationServices NavigationServices { get; } = new NavigationServices();
        public static IDialogServices DialogServices { get; } = new DialogServices();
        public static IPlayerServices PlayerServices { get; } = new PlayerServices();

        public App()
        {
            InitializeComponent();

            // Register Pages
            NavigationServices.RegisterPages(ViewNames.MAIN_PAGE, typeof(MainPage));
            NavigationServices.RegisterPages(ViewNames.PLAYER_CONFIG_PAGE, typeof(PlayerConfigView));
            NavigationServices.RegisterPages(ViewNames.EDIT_SCORES_PAGE, typeof(EditScoresView));
            NavigationServices.RegisterPages(ViewNames.SCORE_SHEET_PAGE, typeof(ScoreSheetView));

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
