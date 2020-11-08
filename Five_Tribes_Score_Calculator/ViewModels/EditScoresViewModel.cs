using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.Services;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class EditScoresViewModel : BaseViewModel
    {
        // Fields
        private INavigationServices navigationServices = null;
        private IPlayerServices playerServices = null;
        private PlayerModel player = null;

        // Properties
        public ICommand NavigateBackCommand { get; }
        public ICommand ConfirmEditCommand { get; }

        public PlayerModel Player
        {
            get => player;
            set
            {
                player = value;
                OnPropertyChanged(nameof(Player));
            }
        }

        // Constructor
        public EditScoresViewModel(INavigationServices navigationServices, IPlayerServices playerServices)
        {
            // Initialise services
            this.navigationServices = navigationServices;
            this.playerServices = playerServices;

            // Initialise commands
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
            ConfirmEditCommand = new Command(async () => await ConfirmEditAsync(), CanConfirmEdit);
        }

        /// <summary>
        /// Store all scores and navigate to previous page.
        /// </summary>
        /// <returns></returns>
        private async Task ConfirmEditAsync()
        {
            // TO DO: Add confirmation message and store scores to score model

            await navigationServices.GobackAsync();
        }

        /// <summary>
        /// Return true or false to determine if confirm button is clickable.
        /// </summary>
        /// <returns></returns>
        private bool CanConfirmEdit()
        {
            // TO DO: Add logic to decide whether the confirm button can be clicked or not.
            return true;
        }

        /// <summary>
        /// Navigate to previous page.
        /// </summary>
        /// <returns></returns>
        private async Task NavigateBackAsync()
        {
            await navigationServices.GobackAsync();
        }

        /// <summary>
        /// Initialize the player object.
        /// </summary>
        /// <param name="player"></param>
        public override void Initialize(object player)
        {
            if (player != null)
            {
                Player = player as PlayerModel;
            }
        }
    }
}
