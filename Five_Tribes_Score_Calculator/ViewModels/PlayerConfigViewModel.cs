using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Helpers;
using Five_Tribes_Score_Calculator.Services;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class PlayerConfigViewModel : BaseViewModel
    {
        // Fields
        private INavigationServices navigationServices = null;
        private IDialogServices dialogServices = null;
        private IPlayerServices playerServices = null;
        private int playerCount = 0;
        private string playerName = string.Empty;
        private string selectedGender = string.Empty;

        // Properties
        public ICommand NavigateBackCommand { get; }
        public ICommand AddPlayerCommand { get; }
        public ICommand RemovePlayerCommand { get; }
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

        private ObservableCollection<PlayerModel> players;
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
            AddPlayerCommand = new Command(async () => await AddPlayerToList());
            RemovePlayerCommand = new Command(RemovePlayerFromList);
            CalculateScoreCommand = new Command(async () => await CalculateScore(), CanCalculateScore);

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
        public override void Initialize(object playerCount)
        {
            this.playerCount = (int)playerCount;

            // Update CanExecute state
            ((Command)CalculateScoreCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Add a new player with given details to list
        /// </summary>
        private async Task AddPlayerToList()
        {
            // If name and gender are entered
            if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(SelectedGender))
            {
                // If number of player in the list is less than predefined player count
                if (Players.Count < playerCount)
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
                    await dialogServices.ShowErrorAsync(DialogServices.MaxPlayerCountError, playerCount);
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
        /// Remove the selected player
        /// </summary>
        /// <param name="player"></param>
        private void RemovePlayerFromList(object player)
        {
            // Remove the specified player
            playerServices.RemovePlayer(player);

            // Update CanExecute state
            ((Command)CalculateScoreCommand).ChangeCanExecute();
        }

        /// <summary>
        /// Calculate scores for all players and display the winner and allow them to navigate to scoresheet page
        /// </summary>
        /// <returns></returns>
        private async Task CalculateScore()
        {
            bool showDetails = await Application.Current.MainPage.DisplayAlert("Results: ", "Show winner", "See details", "Cancel");

            if (showDetails)
            {
                // TO DO: Navigate to scoresheet page
                await Application.Current.MainPage.DisplayAlert("Go to scoresheet page", "Show score sheet", "OK");
            }
        }

        /// <summary>
        /// Disable button if return false
        /// </summary>
        /// <returns></returns>
        private bool CanCalculateScore()
        {
            // If player count doesn't equal to predefined number of players or any players haven't enter scores
            if (Players.Count != playerCount || Players.Any(p => p.TotalScore == 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
