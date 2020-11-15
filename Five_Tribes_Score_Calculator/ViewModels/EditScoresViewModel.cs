using System;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Services;
using Five_Tribes_Score_Calculator.Models;
using Five_Tribes_Score_Calculator.Helpers;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class EditScoresViewModel : BaseViewModel
    {
        // Fields
        private INavigationServices navigationServices = null;
        private IDialogServices dialogServices = null;
        private PlayerModel player;
        private bool isFieldEnable = false;
        private Dictionary<string, string> scores = null;

        // Properties
        public ICommand NavigateBackCommand { get; }
        public ICommand ConfirmEditCommand { get; }

        public bool IsFieldEnable
        {
            get => isFieldEnable;
            set { isFieldEnable = value; OnPropertyChanged(nameof(IsFieldEnable)); }
        } 

        public PlayerModel Player
        {
            get => player;
            set { player = value; OnPropertyChanged(nameof(Player)); }
        }

        public Dictionary<string, string> Scores
        {
            get => scores;
            set { scores = value; OnPropertyChanged(nameof(Scores)); }
        }

        // Constructor
        public EditScoresViewModel(INavigationServices navigationServices, IDialogServices dialogServices)
        {
            // Initialise services
            this.navigationServices = navigationServices;
            this.dialogServices = dialogServices;

            // Initialise commands
            NavigateBackCommand = new Command(async () => await NavigateBackAsync());
            ConfirmEditCommand = new Command(async () => await ConfirmEditAsync());
        }

        /// <summary>
        /// Initialize the player, scores object and enable edit score fields
        /// </summary>
        /// <param name="player"></param>
        public override void Initialize(object player, object game)
        {
            if (player != null)
            {
                Player = player as PlayerModel;

                if (Player.Scores is null)
                {
                    // Create an empty dictionary
                    Scores = new Dictionary<string, string>()
                    {
                        { "COINS", "" },
                        { "VIZIERS", "" },
                        { "ELDERS", "" },
                        { "DJINNS", "" },
                        { "TREES", "" },
                        { "PALACES", "" },
                        { "CAMELS", "" },
                        { "RESOURCES", "" },
                        { "ARTISANS", "" },
                        { "ITEMS", "" },
                    };
                }
                else
                {
                    Scores = Player.Scores;
                }
            }

            // If game type is base game, disable fields.
            if (game != null)
            {
                IsFieldEnable = !((GameModel)game).GameType.Equals(GameTypes.FT);
            }
        }

        /// <summary>
        /// Store all scores and navigate to previous page.
        /// </summary>
        /// <returns></returns>
        private async Task ConfirmEditAsync()
        {
            if (IsValueValidated())
            {
                // Reformat values before saving it (in case there are some values with leading zeros)
                Scores.Where(s => s.Value.Contains("0")).Select(s => s.Key).ToList().ForEach(k => Scores[k] = Convert.ToInt32(Scores[k]).ToString());

                // Update empty scores to 0
                Scores.Where(s => string.IsNullOrWhiteSpace(s.Value)).Select(s => s.Key).ToList().ForEach(k => Scores[k] = "0");

                // Record scores against the player.
                Player.Scores = Scores;

                // Mark this user as complete.
                Player.MarkAsComplete = true;

                // Notify the scores has been saved.
                MessagingCenter.Send(this, Messages.MarkAsCompleteMessage);

                // Clear entries
                ClearScrores();

                await navigationServices.GobackAsync();
            }
            else
            {
                // Show mandatory fields must be entered message.
                await dialogServices.ShowErrorAsync(Scores, IsFieldEnable);
            }
        }

        /// <summary>
        /// Navigate to previous page.
        /// </summary>
        /// <returns></returns>
        private async Task NavigateBackAsync()
        {
            //Clear entries
            ClearScrores();

            await navigationServices.GobackAsync();
        }

        /// <summary>
        /// Return true or false to determine if scores contain decimal point
        /// </summary>
        /// <returns></returns>
        private bool IsValueValidated()
        {
            if (Scores["COINS"].Contains('.') || Scores["VIZIERS"].Contains('.') || Scores["ELDERS"].Contains('.') ||
                Scores["DJINNS"].Contains('.') || Scores["TREES"].Contains('.') || Scores["PALACES"].Contains('.') ||
                Scores["CAMELS"].Contains('.') || Scores["RESOURCES"].Contains('.'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Clear out entries' value.
        /// </summary>
        private void ClearScrores()
        {
            Scores = null;
        }
    }
}
