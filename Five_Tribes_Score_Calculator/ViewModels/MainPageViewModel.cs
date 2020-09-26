using System;
using System.Windows.Input;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.Services;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // Properties
        private int _maximumPlayers = 0;
        private PlayerCountServices _playCountServices = null;
        private DialogServices _dialogServices = null;
        private NavigationServices _navigationServices = null;

        public ICommand SelectGameType { get; }

        public ICommand SubmitSettings { get; }

        public GameModel SelectedGame { get; } = new GameModel();

        private string _gameName = "No Game Selected";
        public string GameName
        {
            get => _gameName;
            private set
            {
                _gameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }

        private List<string> _playerCountList = new List<string>();
        public List<string> PlayerCountList
        {
            get => _playerCountList;
            private set
            {
                _playerCountList = value;
                OnPropertyChanged(nameof(PlayerCountList));
            }
        }

        private string _selectedPlayerCount = string.Empty;
        public string SelectedPlayerCount
        {
            get => _selectedPlayerCount;
            set
            {
                _selectedPlayerCount = value != null ? value : string.Empty;
                SelectedGame.PlayerCount = _playCountServices.PlayerCountDic[_selectedPlayerCount];
                OnPropertyChanged(nameof(SelectedPlayerCount));
            }
        }

        //Constructor
        public MainPageViewModel()
        {
            // Initialize services
            _playCountServices = new PlayerCountServices();
            _dialogServices = new DialogServices();
            _navigationServices = new NavigationServices();

            // Initialize commands
            SelectGameType = new Command<GameTypes>(OnSelectGameType);
            SubmitSettings = new Command(async () => await OnSubmitSettings());

            // Initialize picker items
            PlayerCountList = _playCountServices.PopulatePickerItems(_maximumPlayers);
        }

        /// <summary>
        /// Set which game is selected and set maximum number of players
        /// </summary>
        /// <param name="gameType"></param>
        private void OnSelectGameType(GameTypes gameType)
        {
            // Update game model
            SelectedGame.GameType = gameType;

            // Set maximum players
            _maximumPlayers = 4;

            // Set selected game name
            switch (SelectedGame.GameType)
            {
                case GameTypes.FT:
                    GameName = "Five Tribes Base Game";
                    break;
                case GameTypes.AQ:
                    GameName = "The Artisans Of Naqala";
                    break;
                case GameTypes.WS:
                    GameName = "Whims Of The Sultan";
                    _maximumPlayers = 5;
                    break;
            }

            // Populate picker items
            PlayerCountList = _playCountServices.PopulatePickerItems(_maximumPlayers);
        }

        /// <summary>
        /// Navigate to next page
        /// </summary>
        private async Task OnSubmitSettings()
        {
            if (SelectedGame.GameType != null && SelectedGame.PlayerCount != 0)
            {
                return;
            }
            else
            {
                await _dialogServices.ShowError(SelectedGame);
            }
        }
    }
}
