using System;
using System.Globalization;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Double rounding converter.
    /// </summary>
    public class DoubleRoundingConverter : IValueConverter
    {
        /// <inheritDoc />
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return Round( ( double )value, parameter );
        }

        /// <inheritDoc />
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return Round( ( double )value, parameter );
        }

        private double Round( double number, object parameter )
        {
            double precision = 1;

            // Assume parameter is string encoding precision.
            if ( parameter != null )
            {
                double.TryParse((string)parameter, out precision);
            }
            return precision * Math.Round( number / precision );
        }
    }
}
