using AP.MobileToolkit.Http;

namespace AP.MobileToolkit.Http.Tests.Mocks
{
    public class MockApiClientOptions : IApiClientOptions
    {
        public const int TestPort = 3333;
        public string BaseUri => $"http://localhost:{TestPort}";

        public string InstallId => "";
    }
}
