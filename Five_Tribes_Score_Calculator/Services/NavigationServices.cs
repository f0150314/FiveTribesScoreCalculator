using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Services
{
    public class NavigationServices
    {
        // Properties
        private Page _mainPage => Application.Current.MainPage;

        private Dictionary<string, Type> _pageDic { get; }
            = new Dictionary<string, Type>();

        /// <summary>
        /// Register pages in page dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageType"></param>
        public void RegisterPages(string key, Type pageType) => _pageDic[key] = pageType;

        /// <summary>
        /// Navigate to particular page
        /// </summary>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        public async Task NavigateTo(string pageKey)
        {
            // Create a new page instance
            var page = (Page)Activator.CreateInstance(_pageDic[pageKey]);

            await _mainPage.Navigation.PushModalAsync(page);
        }

        /// <summary>
        /// Go back to previous page
        /// </summary>
        /// <returns></returns>
        public async Task Goback() => await _mainPage.Navigation.PopModalAsync();
    }
}
