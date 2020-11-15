using System;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;
using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Views
{
    public partial class PlayerConfigView : ContentPage
    {
        public PlayerConfigView()
        {
            InitializeComponent();

            // Bind view model
            BindingContext = ViewModelLocator.PlayerConfigViewModel;
        }
    }
}
