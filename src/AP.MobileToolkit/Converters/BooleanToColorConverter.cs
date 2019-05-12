using System;
using System.Globalization;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Converts boolean values to color.
    /// </summary>
    public class BooleanToColorConverter : IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToColorConverter"/> class with default colors.
        /// </summary>
        public BooleanToColorConverter()
            : this(Color.Green, Color.Red)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToColorConverter"/> class.
        /// </summary>
        /// <param name="trueColor">True color.</param>
        /// <param name="falseColor">False color.</param>
        public BooleanToColorConverter(Color trueColor, Color falseColor)
        {
            TrueColor = trueColor;
            FalseColor = falseColor;
        }

        /// <summary>
        /// Gets or sets the true color.
        /// </summary>
        public Color TrueColor { get; set; }

        /// <summary>
        /// Gets or sets the false color.
        /// </summary>
        public Color FalseColor { get; set; }

        /// <summary>
        /// Implement this method to convert <paramref name="value"/> to <paramref name="targetType"/> by using <paramref name="parameter"/> and <paramref name="culture"/>.
        /// </summary>
        /// <param name="value">Boolean value.</param>
        /// <param name="targetType">To be added.</param>
        /// <param name="parameter">To be added.</param>
        /// <param name="culture">To be added.</param>
        /// <returns>
        /// Color value for true or false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && targetType == typeof(Color))
            {
                return (bool)value ? TrueColor : FalseColor;
            }

            throw new InvalidConverterUseException("This converter must be used to convert a Boolean to a Xamarin.Forms.Color");
        }

        /// <summary>
        /// Implement this method to convert <paramref name="value"/> back from <paramref name="targetType"/> by using <paramref name="parameter"/> and <paramref name="culture"/>.
        /// </summary>
        /// <param name="value">To be added.</param><param name="targetType">To be added.</param><param name="parameter">To be added.</param><param name="culture">To be added.</param>
        /// <returns>
        /// True if the value is true color, otherwise false.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Color) || targetType != typeof(bool))
            {
                throw new InvalidConverterUseException("This converter must be used to convert a Boolean to a Xamarin.Forms.Color");
            }

            return value is Color && (Color)value == TrueColor;
        }
    }
}
