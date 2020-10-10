using System.Windows.Input;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.Services;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // Private fields
        private int maximumPlayers = 0;
        private string selectedPlayerCount = string.Empty;
        private string gameName = "No Game Selected";
        private List<string> playerCountList = new List<string>();
        private PlayerCountGenerator playerCountGenerator = new PlayerCountGenerator();
        private INavigationServices navigationServices = null;
        private IDialogServices dialogServices = null;

        // Properties
        public ICommand SelectGameCommand { get; }
        public ICommand SubmitSettingCommand { get; }
        public GameModel SelectedGame { get; } = new GameModel();

        public string GameName
        {
            get => gameName;
            private set
            {
                gameName = value;
                OnPropertyChanged(nameof(GameName));
            }
        }

        public List<string> PlayerCountList
        {
            get => playerCountList;
            private set
            {
                playerCountList = value;
                OnPropertyChanged(nameof(PlayerCountList));
            }
        }

        public string SelectedPlayerCount
        {
            get => selectedPlayerCount;
            set
            {
                // Set playCount
                selectedPlayerCount = value != null ? value : string.Empty;
                SelectedGame.PlayerCount = playerCountGenerator.PlayerCountDic[selectedPlayerCount];
                OnPropertyChanged(nameof(SelectedPlayerCount));
            }
        }

        //Constructor
        public MainPageViewModel(INavigationServices navigationServices, IDialogServices dialogServices)
        {
            //Initialize services
            this.navigationServices = navigationServices;
            this.dialogServices = dialogServices;

            // Initialize commands
            SelectGameCommand = new Command<GameTypes>(SelectGameType);
            SubmitSettingCommand = new Command(async () => await SubmitSettingAsync());

            // Initialize picker list
            PlayerCountList = playerCountGenerator.PopulatePickerItems(maximumPlayers);
        }

        /// <summary>
        /// Set which game is selected and set maximum number of players
        /// </summary>
        /// <param name="gameType"></param>
        private void SelectGameType(GameTypes gameType)
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
            PlayerCountList = playerCountGenerator.PopulatePickerItems(maximumPlayers);
        }

        /// <summary>
        /// Navigate to next page or show error message if config was not set up.
        /// </summary>
        private async Task SubmitSettingAsync()
        {
            if (SelectedGame.GameType != null && SelectedGame.PlayerCount != 0)
            {
                await navigationServices.NavigateToAsync(ViewNames.PLAYER_CONFIG_PAGE, SelectedGame.PlayerCount);
            }
            else
            {
                await dialogServices.ShowErrorAsync<GameModel>(SelectedGame);
            }
        }
    }
}
