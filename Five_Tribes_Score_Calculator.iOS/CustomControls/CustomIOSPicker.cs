using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Five_Tribes_Score_Calculator.iOS.CustomControls;

[assembly: ExportRendererAttribute(typeof(Picker), typeof(CustomIOSPicker))]
namespace Five_Tribes_Score_Calculator.iOS.CustomControls
{
    public class CustomIOSPicker : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TextAlignment = UITextAlignment.Center;
                Control.Layer.BorderColor = Color.DarkGray.ToCGColor();
                Control.Layer.BorderWidth = 1f;
                Control.Layer.CornerRadius = 5;
            }
        }
    }
}
