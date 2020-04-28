using AP.MobileToolkit.Converters;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class InverseBooleanConverterFixture : ConverterBaseFixture
    {
        public InverseBooleanConverterFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void TrueConvertersToFalse()
        {
            IValueConverter converter = new InverseBooleanConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(true, typeof(bool), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.IsType<bool>(output);
            Assert.False((bool)output);
        }

        [Fact]
        public void FalseConvertersToTrue()
        {
            IValueConverter converter = new InverseBooleanConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(false, typeof(bool), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.IsType<bool>(output);
            Assert.True((bool)output);
        }
    }
}
