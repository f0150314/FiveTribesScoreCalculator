using System;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Services;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class ScoreSheetViewModel : BaseViewModel
    {
        // Fields
        private INavigationServices navigationServices = null;
        private ObservableCollection<PlayerModel> players;

        // Properties
        public ICommand NavigateBackCommand { get; set; }
        public ObservableCollection<PlayerModel> Players
        {
            get => players;
            private set
            {
                players = value;

                // This is required because of the assignment, if we add items to Players directly, you don't need this for ObservabaleCollection
                OnPropertyChanged(nameof(Players));
            }
        }

        // Constructor
        public ScoreSheetViewModel(INavigationServices navigationServices)
        {
            // Initialize commands
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());

            // Initialize services
            this.navigationServices = navigationServices;
        }

        /// <summary>
        /// Initialize player list.
        /// </summary>
        /// <param name="players"></param>
        public override void Initialize(object players)
        {
            // Passing players as a parameter to this view model and initialize the player list.
            if (players != null)
            {
                Players = (ObservableCollection<PlayerModel>)players;
            }
        }

        /// <summary>
        /// Navigate to previous page
        /// </summary>
        /// <returns></returns>
        private async Task NavigateBackAsync()
        {
            await navigationServices.GobackAsync();
        }
    }
}
