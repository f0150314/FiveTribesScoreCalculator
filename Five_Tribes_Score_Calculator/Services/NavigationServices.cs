using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Five_Tribes_Score_Calculator.ViewModels;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Services
{
    public class NavigationServices : INavigationServices
    {
        // Properties
        private Page MainPage => Application.Current.MainPage;

        private Dictionary<string, Type> pageDic { get; }
            = new Dictionary<string, Type>();

        /// <summary>
        /// Register pages in page dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageType"></param>
        public void RegisterPages(string key, Type pageType) => pageDic[key] = pageType;

        /// <summary>
        /// Navigate to particular page
        /// </summary>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        public async Task NavigateToAsync(string pageKey, object parameter = null)
        {
            // Create a new page instance
            var page = (Page)Activator.CreateInstance(pageDic[pageKey]);

            await MainPage.Navigation.PushModalAsync(page);

            // Initialize passed parameter
            ((BaseViewModel)page.BindingContext).Initialize(parameter);
        }

        /// <summary>
        /// Go back to previous page
        /// </summary>
        /// <returns></returns>
        public async Task GobackAsync() => await MainPage.Navigation.PopModalAsync();
    }
}
