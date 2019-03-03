using AP.MobileToolkit.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public class UpperTextConverterFixture : ConverterBaseFixture
    {
        public UpperTextConverterFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Theory]
        [InlineData("foo", "FOO")]
        [InlineData("Foo", "FOO")]
        [InlineData("FOO", "FOO")]
        [InlineData(null, "")]
        public void ConvertProducesExpectedOutput(string input, string expected)
        {
            var converter = new UpperTextConverter();
            object output = null;
            var exception = Record.Exception(() => { output = converter.Convert(input, typeof(string), null, CurrentCulture); });

            Assert.Null(exception);
            Assert.NotNull(output);
            Assert.Equal(expected, output);
        }
    }
}
