using System;
using System.Windows.Input;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        //Properties
        public ICommand SelectGameType { get; }
        public GameModel SelectedGame { get; }
        public string GameName { get; private set; }

        //Constructor
        public MainPageViewModel()
        {
            //Initialize command with type for parameters
            SelectGameType = new Command<GameTypes>(OnSelectGameType);

            //Initialize game model
            SelectedGame = new GameModel();

            //Initialize game name
            GameName = "No Game";
        }

        //Select Game Type
        private void OnSelectGameType(GameTypes gameType)
        {
            //Update game model
            SelectedGame.GameType = gameType;

            //Set selected game name
            switch (SelectedGame.GameType)
            {
                case GameTypes.FT:
                    GameName = "Five Tribes";
                    break;
                case GameTypes.AQ:
                    GameName = "Artisans Of Naqala";
                    break;
                case GameTypes.WS:
                    GameName = "Whims Of Sultan";
                    break;
            }

            //Update databinding
            OnPropertyChanged(nameof(GameName));
        }
    }
}
