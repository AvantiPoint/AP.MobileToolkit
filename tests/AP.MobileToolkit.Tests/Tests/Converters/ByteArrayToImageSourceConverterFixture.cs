using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AP.MobileToolkit.Converters;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class ByteArrayToImageSourceConverterFixture : ConverterBaseFixture
    {
        public ByteArrayToImageSourceConverterFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task ConvertedImageSourceReturnsSameImage()
        {
            var image = GetAvantiPointLogo();
            var converter = new ByteArrayToImageSourceConverter();
            StreamImageSource imageSource = null;
            var ex = Record.Exception(() => imageSource = (StreamImageSource)converter.Convert(image, typeof(ImageSource), null, CurrentCulture));
            Assert.Null(ex);
            Assert.NotNull(imageSource);
            var stream = await imageSource.Stream(CancellationToken.None);
            var convertedStream = ReadFully(stream);
            Assert.Equal(image, convertedStream);
        }

        [Fact]
        public void NullValueReturnsNull()
        {
            var converter = new ByteArrayToImageSourceConverter();
            object value = null;
            var ex = Record.Exception(() => value = converter.Convert(null, typeof(ImageSource), null, CurrentCulture));
            Assert.Null(ex);
            Assert.Null(value);
        }

        [Fact]
        public void ConvertBackThrowsNotImplementedException()
        {
            var converter = new ByteArrayToImageSourceConverter();
            var ex = Record.Exception(() => converter.ConvertBack(null, typeof(byte[]), null, CurrentCulture));
            Assert.IsType<NotImplementedException>(ex);
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private byte[] GetAvantiPointLogo() =>
            File.ReadAllBytes(Path.Combine("Images", "AvantiPoint.png"));
    }
}
