using System;
using System.Collections.ObjectModel;
using Five_Tribes_Score_Calculator.Models;
namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class ScoreSheetViewModel : BaseViewModel
    {
        public ObservableCollection<PlayerModel> Players { get; set; }

        public ScoreSheetViewModel()
        {
        }

        /// <summary>
        /// Initialize player list.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Initialize(object parameter)
        {
            // Passing players as a parameter to this view model and initialize the player list.
            if (parameter != null)
            {
                Players = (ObservableCollection<PlayerModel>)parameter;
            }
        }
    }
}
