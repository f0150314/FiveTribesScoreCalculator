using System;
using System.Collections.Generic;
using Five_Tribes_Score_Calculator.Helpers;

using Xamarin.Forms;

namespace Five_Tribes_Score_Calculator.Views
{
    public partial class ScoreSheetView : ContentPage
    {
        public ScoreSheetView()
        {
            InitializeComponent();

            // Bind view model
            BindingContext = ViewModelLocator.ScoreSheetViewModel;
        }
    }
}
