using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace AP.MobileToolkit.Platform.iOS.Extensions
{
    /// <summary>
    /// Extension class for Colors.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts the UIColor to a Xamarin Color object.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="defaultColor">The default color.</param>
        /// <returns>UIColor.</returns>
        public static UIColor ToUIColorOrDefault( this Xamarin.Forms.Color color, UIColor defaultColor )
        {
            if( color == Xamarin.Forms.Color.Default )
                return defaultColor;

            return color.ToUIColor();
        }
    }
}
