using System;
using System.Windows.Input;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;
using System.Linq;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // Properties
        private int maximumPlayers = 0;

        public ICommand SelectGameType { get; }

        public GameModel SelectedGame { get; } = new GameModel();

        public Dictionary<string, int> PlayerCountDic { get; private set; } = new Dictionary<string, int>();

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
                SelectedGame.PlayerCount = PlayerCountDic[_selectedPlayerCount];
                OnPropertyChanged(nameof(SelectedPlayerCount));
            }
        }

        //Constructor
        public MainPageViewModel()
        {
            // Initialize command with type for parameters
            SelectGameType = new Command<GameTypes>(OnSelectGameType);

            // Initialize picker items
            PlayerCountList = PopulatePickerItems(maximumPlayers);
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
            maximumPlayers = 4;

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
                    maximumPlayers = 5;
                    break;
            }

            // Populate picker items
            PlayerCountList = PopulatePickerItems(maximumPlayers);
        }

        /// <summary>
        /// Populate a list of play count in the picker control
        /// </summary>
        /// <param name="maximumPlayers"></param>
        /// <returns>A list of keys in PlayCountDic</returns>
        private List<string> PopulatePickerItems(int maximumPlayers)
        {
            // Clear dictionary
            PlayerCountDic.Clear();

            string itemKey = string.Empty;

            // Add items
            for (int i = 0; i <= maximumPlayers; i++)
            {
                // No one player option
                if (i == 1)
                {
                    continue;
                }
                // If player more than 1, set item key
                else if (i != 0)
                {
                    itemKey = string.Format("{0} Players", i);
                }

                PlayerCountDic.Add(itemKey, i);
            }

            return PlayerCountDic.Keys.ToList();
        }
    }
}
