using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Services;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class PlayerConfigViewModel : BaseViewModel
    {
        // Fields
        private INavigationServices navigationServices = null;
        private IDialogServices dialogServices = null;
        private IPlayerServices playerServices = null;
        private GameModel gameModel = null;
        private string playerName = string.Empty;
        private string selectedGender = string.Empty;
        private ObservableCollection<PlayerModel> players;

        // Properties
        public ICommand NavigateBackCommand { get; }
        public ICommand AddPlayerCommand { get; }
        public ICommand RemovePlayerCommand { get; }
        public ICommand NavigateToEditPage { get; }
        public ICommand CalculateScoreCommand { get; }

        public string PlayerName
        {
            get => playerName;
            set
            {
                playerName = value;
                OnPropertyChanged(nameof(PlayerName));
            }
        }

        public string SelectedGender
        {
            get => selectedGender;
            set
            {
                selectedGender = value;
                OnPropertyChanged(nameof(SelectedGender));
            }
        }

        public ObservableCollection<PlayerModel> Players
        {
            get => players;
            set
            {
                players = value;

                // This is required because of the assignment, if we add items to Players directly, you don't need this for ObservabaleCollection
                OnPropertyChanged(nameof(Players));
            }
        }

        // Constructor
        public PlayerConfigViewModel(INavigationServices navigationServices, IDialogServices dialogServices, IPlayerServices playerServices)
        {
            // Initialize services
            this.navigationServices = navigationServices;
            this.dialogServices = dialogServices;
            this.playerServices = playerServices;

            // Initialize command
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
            AddPlayerCommand = new Command(async () => await AddPlayerToListAsync());
            RemovePlayerCommand = new Command(RemovePlayerFromList);
            NavigateToEditPage = new Command<PlayerModel>(async (player) => await NavigateToEditScorePageAsync(player));
            CalculateScoreCommand = new Command(async () => await CalculateScoreAsync(), CanCalculateScore);

            // Initialize players
            RefreshPlayerList(null);

            // Subscribe messages
            MessagingCenter.Subscribe<PlayerServices>(this, Messages.AddPlayerMessage, RefreshPlayerList);
            MessagingCenter.Subscribe<PlayerServices>(this, Messages.RemovePlayerMessage, RefreshPlayerList);
        }

        /// <summary>
        /// Refresh a player list
        /// </summary>
        private void RefreshPlayerList(PlayerServices sender)
        {
            Players = new ObservableCollection<PlayerModel>(playerServices.GetAllPlayers());
        }

        /// <summary>
        /// Navigate to previous page
        /// </summary>
        /// <returns></returns>
        private async Task NavigateBackAsync()
        {
            await navigationServices.GobackAsync();
        }

        /// <summary>
        /// Get maximum number of players from last page
        /// </summary>
        /// <param name="parameter"></param>
        public override void Initialize(object gameModel)
        {
            if (gameModel != null)
            {
                this.gameModel = (GameModel)gameModel;
            }

            // Update CanExecute state
            ((Command)CalculateScoreCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Add a new player with given details to list
        /// </summary>
        private async Task AddPlayerToListAsync()
        {
            // If name and gender are entered
            if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(SelectedGender))
            {
                // If number of player in the list is less than predefined player count
                if (Players.Count < gameModel.PlayerCount)
                {
                    // If no duplicate name
                    if (!Players.Any(p => p.Name.ToLower() == PlayerName.ToLower().Trim()))
                    {
                        // Add a new player with given details
                        playerServices.AddPlayer(PlayerName.Trim(), SelectedGender);

                        // Clear out entry text and gender drop down after adding the player
                        PlayerName = string.Empty;
                        SelectedGender = string.Empty;
                    }
                    else
                    {
                        // Show name already exists error
                        await dialogServices.ShowErrorAsync(DialogServices.DuplicateNameError);
                    }
                }
                else
                {
                    // Show can't exceed maximum number of players error
                    await dialogServices.ShowErrorAsync(DialogServices.MaxPlayerCountError, gameModel.PlayerCount);
                }
            }
            else
            {
                // Show fields not entered error
                await dialogServices.ShowErrorAsync(DialogServices.MissingFieldError, null, PlayerName, SelectedGender);
            }

            // Update CanExecute state
            ((Command)CalculateScoreCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Redirect the user to enter score page.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private async Task NavigateToEditScorePageAsync(PlayerModel player)
        {
            if (player != null && gameModel != null)
            {
                // If it is base game, set scores of artisans and items to 0.
                // The preprocess was done because dictionary value shown in the entry cannot be updated by OnNotifiyPropertyChanged event.
                if (player.Scores != null && gameModel.GameType.Equals(GameTypes.FT))
                {
                    player.Scores["ARTISANS"] = "0" ;
                    player.Scores["ITEMS"] = "0";
                }

                await navigationServices.NavigateToAsync(ViewNames.EDIT_SCORES_PAGE, player, gameModel);
            }
        }

        /// <summary>
        /// Remove the selected player
        /// </summary>
        /// <param name="player"></param>
        private void RemovePlayerFromList(object player)
        {
            if (player != null)
            {
                // Remove the specified player
                playerServices.RemovePlayer(player);
            }

            // Update CanExecute state
            ((Command)CalculateScoreCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Calculate scores for all players and display the winner and allow them to navigate to scoresheet page
        /// </summary>
        /// <returns></returns>
        private async Task CalculateScoreAsync()
        {
            // Final check for game type, set score of artisans and items if is base game.
            UpdateScoreIfBaseGame();

            // Show winner/winners
            FindWinners();
            bool showDetails = await dialogServices.ShowWinnerAsync(Players);

            // If show details is clicked navigate to scoresheet page
            if (showDetails)
            {
                // Navigate to scoresheet page
                await navigationServices.NavigateToAsync(ViewNames.SCORE_SHEET_PAGE, Players);
            }
        }

        /// <summary>
        /// Disable button if return false
        /// </summary>
        /// <returns></returns>
        private bool CanCalculateScore()
        {
            // If player count doesn't equal to predefined number of players or there are players that have not been marked as complete.
            // The ?. is to prevent null reference of game model when initialiing the CanExecute method.
            if (Players.Count != gameModel?.PlayerCount || Players.Any(p => p.MarkAsComplete != true))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Find winners among players
        /// </summary>
        /// <returns></returns>
        private void FindWinners()
        {
            // Reset winners (In case they change their scores)
            Players.ToList().ForEach(p => p.IsWinner = false);

            // Get total scores
            List<int> totalScoreList = new List<int>();
            Players.ToList().ForEach(p => totalScoreList.Add(p.TotalScore));

            // Set winners using total scores
            Players.Where(p => p.TotalScore == totalScoreList.Max()).ToList().ForEach(p => p.IsWinner = true);
        }

        /// <summary>
        /// Update score of artisans and precious items to zero if base game is selected.
        /// </summary>
        private void UpdateScoreIfBaseGame()
        {
            if (gameModel.GameType.Equals(GameTypes.FT))
            {
                foreach (var player in Players)
                {
                    player.Scores
                        .Where(s => s.Key.Equals("ARTISANS") || s.Key.Equals("ITEMS"))  // Find ARTISANS and ITEMS scores
                        .Select(s => s.Key).ToList()                                    // Get their corresponding key
                        .ForEach(k => player.Scores[k] = "0");                          // Update scores to 0
                }
            }
        }
    }
}
