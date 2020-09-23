using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Five_Tribes_Score_Calculator.Droid.CustomControls;
using Android.Views;
using Android.Widget;
using Android.Content;
using Android.Graphics.Drawables;

[assembly: ExportRendererAttribute(typeof(Picker), typeof(CustomAndroidPicker))]
namespace Five_Tribes_Score_Calculator.Droid.CustomControls
{
    public class CustomAndroidPicker : PickerRenderer
    {
        public CustomAndroidPicker(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(5f);
                gradientDrawable.SetStroke(1, Android.Graphics.Color.DarkGray);
                Control.SetBackground(gradientDrawable);

                Control.Gravity = GravityFlags.CenterHorizontal;
            }
        }
    }
}
