using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Five_Tribes_Score_Calculator.Models;
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
        private string playerName = string.Empty;
        private string selectedGender = string.Empty;

        // Properties
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

        public ICommand NavigateBack { get; }

        public ICommand AddPlayer { get; }

        public ICommand DeletePlayer { get; }

        public GameModel SelectedGame { get; private set; }

        public ObservableCollection<PlayerModel> Players { get; set; }

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
            //AddPlayer = new Command(AddPlayerToList);
            DeletePlayer = new Command(DeletePlayerFromList);

            // Initialize players
            //Players = new ObservableCollection<PlayerModel>(playerServices.GetAllPlayers());

            Players = new ObservableCollection<PlayerModel>()
            {
                new PlayerModel { Name = "John", Gender = "Male"},
                new PlayerModel { Name = "Chloe", Gender = "Female" }
            };
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
        /// Get selected game from last page
        /// </summary>
        /// <param name="parameter"></param>
        public override void Initialize(object parameter)
        {
            SelectedGame = (GameModel)parameter;
        }

        /// <summary>
        /// Add a new player with given details to list
        /// </summary>
        private async Task AddPlayerToList()
        {
            //// If player number less than predefined player count
            //if (Players.Count < SelectedGame.PlayerCount)
            //{
            //    // If name and gender are entered
            //    if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(SelectedGender))
            //    {
            //        // If no duplicate name
            //        if (!Players.Any(p => p.Name == PlayerName))
            //        {
            //            playerServices.AddPlayer(PlayerName, SelectedGender);

            //            //Clear out entry text and gender drop down after adding the player
            //            PlayerName = string.Empty;
            //            SelectedGender = string.Empty;
            //        }
            //        else
            //        {
            //            //To Do: show duplicate name
            //            await dialogServices.ShowSubmitErrorAsync(SelectedGame);
            //        }
            //    }
            //    else
            //    {
            //        // To Do: show name or gender not entered
            //        await dialogServices.ShowSubmitErrorAsync(SelectedGame);
            //    } 
            //}
            //else
            //{
            //    //To Do: show error message.
            //    await dialogServices.ShowSubmitErrorAsync(SelectedGame);
            //}



            if (Players.Count < SelectedGame.PlayerCount)
            {
                // If name and gender are entered
                if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(SelectedGender))
                {
                    // If no duplicate name
                    if (!Players.Any(p => p.Name == PlayerName))
                    {
                        PlayerModel player = new PlayerModel { Name = PlayerName, Gender = SelectedGender };
                        Players.Add(player);

                        //Clear out entry text and gender drop down after adding the player
                        PlayerName = string.Empty;
                        SelectedGender = string.Empty;
                    }
                    else
                    {
                        //To Do: show duplicate name
                        await dialogServices.ShowErrorAsync(DialogServices.DuplicateNameError);
                    }
                }
                else
                {
                    // To Do: show name or gender not entered
                    await dialogServices.ShowErrorAsync(DialogServices.MissingFieldError, null, PlayerName, SelectedGender);
                }
            }
            else
            {
                //To Do: show error message.
                await dialogServices.ShowErrorAsync(DialogServices.MaxPlayerCountError, SelectedGame.PlayerCount);
            }
        }

        /// <summary>
        /// Delete the selected player
        /// </summary>
        /// <param name="player"></param>
        private void DeletePlayerFromList(object player)
        {
            //playerServices.DeletePlayer(player);

            Players.Remove((PlayerModel)player);
        }
    }
}
