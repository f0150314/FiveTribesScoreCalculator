using System;
using System.Threading.Tasks;

namespace Five_Tribes_Score_Calculator.Services
{
    public interface INavigationServices
    {
        void RegisterPages(string key, Type pageType);

        Task NavigateToAsync(string pageKey, object parameter = null);

        Task GobackAsync();
    }
}
