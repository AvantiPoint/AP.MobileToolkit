using System;
using System.Globalization;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Provides a strongly typed base class for <see cref="IValueConverter" />
    /// </summary>
    /// <typeparam name="TReturn">The type that should be converted to</typeparam>
    /// <typeparam name="TInput">The type that should be converted from</typeparam>
    public abstract class ConverterBase<TReturn, TInput> : IValueConverter
    {
        /// <summary>
        /// Converts the <typeparamref name="TInput"/> to <typeparamref name="TReturn"/>
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The requested <see cref="CultureInfo" /></param>
        /// <returns>The <typeparamref name="TReturn"/></returns>
        protected abstract TReturn Convert(TInput value, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts from the <typeparamref name="TReturn"/> back to <typeparamref name="TInput"/>
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The requested <see cref="CultureInfo" /></param>
        /// <returns>The <typeparamref name="TInput"/></returns>
        protected abstract TInput ConvertBack(TReturn value, object parameter, CultureInfo culture);

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(TReturn) && value is TInput inputValue)
                return Convert(inputValue, parameter, culture);

            return default(TReturn);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == typeof(TInput) && value is TReturn returnValue)
                return ConvertBack(returnValue, parameter, culture);

            return default(TInput);
        }
    }
}
