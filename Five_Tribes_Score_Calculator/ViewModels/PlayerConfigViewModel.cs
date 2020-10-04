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
        public ICommand NavigateBack { get; }
        public ICommand AddPlayer { get; }
        public ICommand RemovePlayer { get; }

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
            NavigateBack = new Command(async () => await OnNavigateBackAsync());
            AddPlayer = new Command(async () => await AddPlayerToList());
            RemovePlayer = new Command(RemovePlayerFromList);

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
        private async Task OnNavigateBackAsync()
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
        }

        /// <summary>
        /// Add a new player with given details to list
        /// </summary>
        private async Task AddPlayerToList()
        {
            // If number of player in the list is less than predefined player count
            if (Players.Count < playerCount)
            {
                // If name and gender are entered
                if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(SelectedGender))
                {
                    // If no duplicate name
                    if (!Players.Any(p => p.Name == PlayerName))
                    {
                        // Add a new player with given details
                        playerServices.AddPlayer(PlayerName, SelectedGender);

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
                    // Show fields not entered error
                    await dialogServices.ShowErrorAsync(DialogServices.MissingFieldError, null, PlayerName, SelectedGender);
                }
            }
            else
            {
                // Show maximum number of player is reached error
                await dialogServices.ShowErrorAsync(DialogServices.MaxPlayerCountError, playerCount);
            }
        }

        /// <summary>
        /// Remove the selected player
        /// </summary>
        /// <param name="player"></param>
        private void RemovePlayerFromList(object player)
        {
            // Remove the specified player
            playerServices.RemovePlayer(player);
        }
    }
}
