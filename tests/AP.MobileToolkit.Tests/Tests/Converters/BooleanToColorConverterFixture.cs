using AP.MobileToolkit.Converters;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class BooleanToColorConverterFixture : ConverterBaseFixture
    {
        public BooleanToColorConverterFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void TrueBooleanConvertsToTrueColor()
        {
            var converter = new BooleanToColorConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(true, typeof(Color), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.IsType<Color>(output);
            Assert.Equal(converter.TrueColor, output);
        }

        [Fact]
        public void FalseBooleanConvertersToFalseColor()
        {
            var converter = new BooleanToColorConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(false, typeof(Color), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.IsType<Color>(output);
            Assert.Equal(converter.FalseColor, output);
        }
    }
}
