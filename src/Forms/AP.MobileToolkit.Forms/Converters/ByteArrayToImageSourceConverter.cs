using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace AP.MobileToolkit.Converters
{
    /// <summary>
    /// Byte array to image source converter.
    /// </summary>
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        /// <inheritDoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return default(ImageSource);
            }
            byte[] bytes = value as byte[];
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        /// <inheritDoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
