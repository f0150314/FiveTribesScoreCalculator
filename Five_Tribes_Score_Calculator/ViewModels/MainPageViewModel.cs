using System.Windows.Input;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // Private fields
        private int _maximumPlayers = 0;
        private PlayerCountGenerator _playerCountGenerator = new PlayerCountGenerator();

        // Properties
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
                // Set playCount
                _selectedPlayerCount = value != null ? value : string.Empty;
                SelectedGame.PlayerCount = _playerCountGenerator.PlayerCountDic[_selectedPlayerCount];
                OnPropertyChanged(nameof(SelectedPlayerCount));
            }
        }

        //Constructor
        public MainPageViewModel()
        {
            // Initialize commands
            SelectGameType = new Command<GameTypes>(OnSelectGameType);
            SubmitSettings = new Command(async () => await OnSubmitSettings());

            // Initialize picker list
            PlayerCountList = _playerCountGenerator.PopulatePickerItems(_maximumPlayers);
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
            PlayerCountList = _playerCountGenerator.PopulatePickerItems(_maximumPlayers);
        }

        /// <summary>
        /// Navigate to next page or show error message if config was not set up.
        /// </summary>
        private async Task OnSubmitSettings()
        {
            if (SelectedGame.GameType != null && SelectedGame.PlayerCount != 0)
            {
                await App.NavigationServices.NavigateTo(ViewNames.PLAYER_CONFIG_PAGE);
            }
            else
            {
                await App.DialogServices.ShowError(SelectedGame);
            }
        }
    }
}
