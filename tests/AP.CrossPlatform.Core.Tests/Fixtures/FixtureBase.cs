using Xunit.Abstractions;

namespace AP.CrossPlatform.Core.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        protected ITestOutputHelper TestOutputHelper { get; }

        public FixtureBase(ITestOutputHelper testOutputHelper)
        {
            TestOutputHelper = testOutputHelper;
        }
    }
}
