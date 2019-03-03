using AP.MobileToolkit.Converters;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class LowerTextConverterFixture : ConverterBaseFixture
    {
        public LowerTextConverterFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Theory]
        [InlineData("foo", "foo")]
        [InlineData("Foo", "foo")]
        [InlineData("FOO", "foo")]
        [InlineData(null, "")]
        public void ConvertProducesExpectedOutput(string input, string expected)
        {
            var converter = new LowerTextConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(input, typeof(string), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.Equal(expected, output);
        }
    }
}
