using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.Models;

namespace Five_Tribes_Score_Calculator.Services
{
    public interface IDialogServices
    {
        Task ShowErrorAsync<T>(T model) where T : BaseModel;

        Task ShowErrorAsync(Dictionary<string, string> dic, bool isFieldEnable);   

        Task ShowErrorAsync(string errorType, int? maxPlayerCount = null, string name = null, string gender = null);

        Task<bool> ShowWinnerAsync(ObservableCollection<PlayerModel> players);
    }
}
