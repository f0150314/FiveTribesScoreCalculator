using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;
using System.Text;

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
        /// Showing error popup 2
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ShowErrorAsync(Dictionary<string, string> scores, bool isFieldEnable)
        {
            title = "Oops! Something is wrong!";
            cancelText = "OK";

            // Find keys of scores with an empty value
            List<string> scoreKeys = scores.Where(s => s.Value.Contains('.')).Select(s => s.Key).ToList();

            // Building an error message
            int index = 0;
            string scoreName = string.Empty;
            StringBuilder message = new StringBuilder("\n");

            scoreKeys.ForEach(k =>
            {
                switch (k)
                {
                    case "COINS":
                        scoreName = "Coins";
                        index += 1;
                        break;
                    case "VIZIERS":
                        scoreName = "Viziers";
                        index += 1;
                        break;
                    case "ELDERS":
                        scoreName = "Elders";
                        index += 1;
                        break;
                    case "DJINNS":
                        scoreName = "Djinns";
                        index += 1;
                        break;
                    case "TREES":
                        scoreName = "Palm trees";
                        index += 1;
                        break;
                    case "PALACES":
                        scoreName = "Palaces";
                        index += 1;
                        break;
                    case "CAMELS":
                        scoreName = "Camels";
                        index += 1;
                        break;
                    case "RESOURCES":
                        scoreName = "Resources";
                        index += 1;
                        break;
                    case "ARTISANS":
                        if (isFieldEnable)
                        {
                            scoreName = "Artisans";
                            index += 1;
                        }
                        else
                        {
                            scoreName = "Skip";
                        }
                        break;
                    case "ITEMS":
                        if (isFieldEnable)
                        {
                            scoreName = "Precious items";
                            index += 1;
                        }
                        else
                        {
                            scoreName = "Skip";
                        }
                        break;
                }

                // This is done to prevent Resources from looping through three times due to every element in the list will call this function
                if (!scoreName.Equals("Skip"))
                {
                    message.AppendFormat("{0}. {1}: Invalid character dot(.).\n", index, scoreName);
                }
            });

            await Application.Current.MainPage.DisplayAlert(title, message.ToString().TrimEnd(), cancelText);
        }

        /// <summary>
        /// Show error popup 3
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

            acceptText = "View score sheet";
            cancelText = "Cancel";

            return await Application.Current.MainPage.DisplayAlert(title, message.TrimEnd(), acceptText, cancelText);
        }
    }
}
