using System.Globalization;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Inverts a boolean value
    /// </summary>
    /// <remarks>Removed unneeded default ctor</remarks>
    public sealed class InverseBooleanConverter : ConverterBase<bool, bool>
    {
        protected override bool Convert(bool value, object parameter, CultureInfo culture) => !value;

        protected override bool ConvertBack(bool value, object parameter, CultureInfo culture) => !value;
    }
}
