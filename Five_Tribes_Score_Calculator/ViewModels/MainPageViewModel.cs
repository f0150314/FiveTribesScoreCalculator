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

        private string _gameName = string.Empty;
        public string GameName
        {
            get => _gameName;
            private set
            {
                _gameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }

        private int _maximumPlayers = 0;
        public int MaximumPlayers
        {
            get => _maximumPlayers;
            private set
            {
                _maximumPlayers = value;
            }
        }

        //Constructor
        public MainPageViewModel()
        {
            //Initialize command with type for parameters
            SelectGameType = new Command<GameTypes>(OnSelectGameType);

            //Initialize game model
            SelectedGame = new GameModel();

            //Initialize game name
            GameName = "No Game Selected";
        }

        //Select Game Type
        private void OnSelectGameType(GameTypes gameType)
        {
            //Update game model
            SelectedGame.GameType = gameType;

            //Set maximum players
            MaximumPlayers = 4;

            //Set selected game name
            switch (SelectedGame.GameType)
            {
                case GameTypes.FT:
                    GameName = "Five Tribes Base Game"
                    break;
                case GameTypes.AQ:
                    GameName = "The Artisans Of Naqala";
                    break;
                case GameTypes.WS:
                    GameName = "Whims Of The Sultan";
                    MaximumPlayers = 5;
                    break;
            }
        }
    }
}
