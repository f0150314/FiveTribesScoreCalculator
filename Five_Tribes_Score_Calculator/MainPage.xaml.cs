using Five_Tribes_Score_Calculator.Helpers;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator
{
    public partial class MainPage : ContentPage
    {
        // Another way to access view model
        //private MainPageViewModel VM => (MainPageViewModel)BindingContext;

        public MainPage()
        {
            InitializeComponent();

            // Bind view model
            BindingContext = ViewModelLocator.MainPageViewModel;
            BindCommandParameter();
        }

        // Set command parameter
        private void BindCommandParameter()
        {
            ButtonFT.CommandParameter = GameTypes.FT;
            ButtonAQ.CommandParameter = GameTypes.AQ;
            ButtonWS.CommandParameter = GameTypes.WS;
        }

        private void ButtonFT_Clicked(System.Object sender, System.EventArgs e)
        {
            FrameFT.BorderColor = Color.White;
            FrameAQ.BorderColor = Color.Default;
            FrameWS.BorderColor = Color.Default;
        }

        private void ButtonAQ_Clicked(System.Object sender, System.EventArgs e)
        {
            FrameFT.BorderColor = Color.Default;
            FrameAQ.BorderColor = Color.White;
            FrameWS.BorderColor = Color.Default;
        }

        private void ButtonWS_Clicked(System.Object sender, System.EventArgs e)
        {
            FrameFT.BorderColor = Color.Default;
            FrameAQ.BorderColor = Color.Default;
            FrameWS.BorderColor = Color.White;
        }
    }
}
