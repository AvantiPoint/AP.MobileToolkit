using System;
using System.Globalization;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Inverts a boolean value
    /// </summary>    
    /// <remarks>Removed unneeded default ctor</remarks>
    public class InverseBooleanConverter : IValueConverter
    {

        /// <inheritDoc />
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return !( ( bool )value );
        }

        /// <inheritDoc />
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return !( ( bool )value );
        }
    }
}
