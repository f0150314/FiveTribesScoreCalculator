using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.ViewModels
{
    public class PlayerConfigViewModel : BaseViewModel
    {
        // Properties
        public ICommand NavigateToPreviousPage { get; set; }

        // Constructor
        public PlayerConfigViewModel()
        {
            // Initialize command
            NavigateToPreviousPage = new Command(async () => await OnNavigateToPreviousPage());
        }

        /// <summary>
        /// Navigate to previous page
        /// </summary>
        /// <returns></returns>
        private async Task OnNavigateToPreviousPage()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
