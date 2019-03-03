using Xunit.Abstractions;

namespace AP.CrossPlatform.Core.Tests.Fixtures
{
    public abstract class FixtureBase
    {
        protected ITestOutputHelper _testOutputHelper { get; }

        public FixtureBase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
    }
}