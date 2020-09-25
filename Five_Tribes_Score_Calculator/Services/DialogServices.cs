using System;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.Models;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Services
{
    public class DialogServices
    {
        private string title = string.Empty;

        private string message = string.Empty;

        private string cancelText = string.Empty;

        /// <summary>
        /// Showing error popup
        /// </summary>
        /// <param name="gameModel"></param>
        /// <returns></returns>
        public async Task ShowError(GameModel gameModel)
        {
            title = "Oops! Something is missing!";
            message = "\n";
            cancelText = "OK";

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

            await Application.Current.MainPage.DisplayAlert(title, message, cancelText);
        }
    }
}
