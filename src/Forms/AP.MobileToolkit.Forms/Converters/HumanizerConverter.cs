using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    public class HumanizerConverter : IValueConverter
    {
        public HumanizerConverter()
        {
            Casing = LetterCasing.Title;
        }

        public LetterCasing Casing { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value}".Humanize(Casing);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value}".Dehumanize();
        }
    }
}
