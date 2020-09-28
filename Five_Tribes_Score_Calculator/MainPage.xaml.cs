using System;
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
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            // Set border of all frames to default color
            foreach (var view in GridInner.Children)
            {
                if (view != null && view is Frame)
                {
                    ((Frame)view).BorderColor = Color.Default;
                }   
            }

            ImageButton button = (ImageButton)sender;

            if (button.Equals(ButtonFT)) {
                FrameFT.BorderColor = Color.White;
            }

            if (button.Equals(ButtonAQ))
            {
                FrameAQ.BorderColor = Color.White;
            }

            if (button.Equals(ButtonWS))
            {
                FrameWS.BorderColor = Color.White;
            }
        }
    }
}
