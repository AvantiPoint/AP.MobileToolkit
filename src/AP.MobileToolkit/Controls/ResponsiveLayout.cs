using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    [ContentProperty(nameof(Children))]
    public partial class ResponsiveLayout : ContentView
    {
        public static readonly BindableProperty RuntimePlatformProperty =
            BindableProperty.Create("Platform", typeof(string), typeof(ResponsiveLayout), null);

        public static readonly BindableProperty DeviceIdiomProperty =
            BindableProperty.Create("Idiom", typeof(TargetIdiom), typeof(ResponsiveLayout), TargetIdiom.Unsupported);

        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(Orientation), typeof(ResponsiveLayout), Orientation.None);

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
        }

        private void SetContent()
        {
        }
    }

    [Flags]
    public enum Orientation
    {
        None = 0,
        Portrait = 1,
        Landscape = 2,
        PortraitUp = 5,
        PortraitDown = 9,
        LandscapeLeft = 18,
        LandscapeRight = 34,
    }
}
