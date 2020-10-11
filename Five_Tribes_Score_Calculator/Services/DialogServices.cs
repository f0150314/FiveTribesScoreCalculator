using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Services 
{
    public class DialogServices : IDialogServices
    {
        // Fields
        private string title = string.Empty;
        private string message = string.Empty;
        private string acceptText = string.Empty;
        private string cancelText = string.Empty;

        // Constants
        public const string MaxPlayerCountError = "MaxPlayerCountError";
        public const string MissingFieldError = "MissingFieldError";
        public const string DuplicateNameError = "DuplicateNameError";


        /// <summary>
        /// Showing error popup
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ShowErrorAsync<T>(T model) where T : BaseModel
        {
            title = "Oops! Something is wrong!";
            message = "\n";
            cancelText = "OK";

            if (model is GameModel)
            {
                GameModel gameModel = model as GameModel;

                if (gameModel.GameType == null)
                {
                    message += "Please select a game";
                }

                if (gameModel.PlayerCount == 0)
                {
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        message += "\n";
                    }
                    message += "Please select number of players";
                }
            }

            await Application.Current.MainPage.DisplayAlert(title, message, cancelText);
        }

        /// <summary>
        /// Show error popup 2
        /// </summary>
        /// <param name="errorType"></param>
        /// <returns></returns>
        public async Task ShowErrorAsync(string errorType, int? maxPlayerCount = null, string name = null, string gender = null)
        {
            title = "Oops! Something is wrong";
            message = "\n";
            cancelText = "OK";

            switch (errorType)
            {
                case MaxPlayerCountError:
                    title += "wrong!";
                    message += $"Maximum number of players is {maxPlayerCount}";
                    break;

                case MissingFieldError:
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        message += "Please enter your name";
                    }

                    if (string.IsNullOrWhiteSpace(gender))
                    {
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            message += "\n";
                        }
                        message += "Please select your gender";
                    }
                    break;

                case DuplicateNameError:
                    message += "The specified name already exists";
                    break;

                default:
                    message = string.Empty;
                    break;
            }

            await Application.Current.MainPage.DisplayAlert(title, message, cancelText);
        }

        /// <summary>
        /// Show winner
        /// </summary>
        /// <param name="playerList"></param>
        /// <returns></returns>
        public async Task<bool> ShowWinnerAsync(ObservableCollection<PlayerModel> playerList)
        {
            int winnerCount = playerList.Count(p => p.IsWinner == true);
            title = winnerCount == 1 ? "WINNER: " : "WINNERS: ";

            message = "\n";

            // Construct message
            foreach (PlayerModel players in playerList)
            {
                if (players.IsWinner)
                {
                    message += $"{players.Name} - Score: {players.TotalScore}\n";
                }
            }

            // Get rid of new line
            if (winnerCount == 1)
            {
                message = message.Substring(0, message.Length - 2);
            }

            acceptText = "Check details";
            cancelText = "Cancel";

            return await Application.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
        }
    }
}
