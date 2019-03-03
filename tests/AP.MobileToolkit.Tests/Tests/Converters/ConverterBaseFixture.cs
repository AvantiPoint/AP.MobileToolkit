using System.Globalization;
using System.Threading;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Converters
{
    public abstract class ConverterBaseFixture : TestBase
    {
        protected CultureInfo CurrentCulture { get; }

        public ConverterBaseFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            CurrentCulture = Thread.CurrentThread.CurrentCulture;
        }
    }
}
