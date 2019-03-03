using System.ComponentModel;
using Android.Content;
using Android.Support.V4.View;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Controls.Platform.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Animation = Android.Animation;

[assembly: ExportRenderer(typeof(MaterialButton), typeof(MaterialButtonRenderer))]
namespace AP.MobileToolkit.Controls.Platform.Droid
{
    public class MaterialButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public static void Init() { }

        public MaterialButtonRenderer(Context context) : base(context)
        {

        }

        /// <summary>
        /// Set up the elevation from load
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Animation.StateListAnimator();

            // set the elevation manually
            UpdateMaterialShadow();

        }

        /// <summary>
        /// Update the elevation when updated from Xamarin.Forms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Elevation")
            {
                UpdateMaterialShadow();
            }
        }

        private void UpdateMaterialShadow()
        {
            var materialButton = (MaterialButton)Element;
            ViewCompat.SetElevation(this, materialButton.Elevation);
            ViewCompat.SetElevation(Control, materialButton.Elevation);
            UpdateLayout();
        }
    }
}
