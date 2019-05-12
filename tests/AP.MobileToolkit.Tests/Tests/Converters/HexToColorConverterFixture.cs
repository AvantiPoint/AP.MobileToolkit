using System.Collections.Generic;
using AP.MobileToolkit.Converters;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class HexToColorConverterFixture : ConverterBaseFixture
    {
        public HexToColorConverterFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ConvertsHexToColor(string hexString, Color expectedColor)
        {
            var converter = new HexToColorConverter();
            var convertedValue = converter.Convert(hexString, typeof(Color), null, CurrentCulture);
            Assert.IsType<Color>(convertedValue);
            Assert.Equal(expectedColor, convertedValue);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { "#FFF0F8FF", Color.AliceBlue },
            new object[] { "#FFF0FFFF", Color.Azure },
            new object[] { "#FFA9A9A9", Color.DarkGray }
        };
    }
}
