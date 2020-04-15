using System.Threading.Tasks;
using Prism.Logging;
using Xamarin.Essentials.Interfaces;

namespace AP.MobileToolkit.Http.Tests.Mocks
{
    public class MockApiClient : ApiClient
    {
        public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiZ2l2ZW5fbmFtZSI6IkpvaG4iLCJmYW1pbHlfbmFtZSI6IkRvZSIsIm9pZCI6ImM2NTJkZWNiLTM3NGUtNGEzNC1iNmUwLWMwZDM3NzQzNmE3MSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        public MockApiClient(IApiClientOptions options, ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
            : base(options, logger, appInfo, deviceInfo)
        {
        }

        public bool DidGetToken => GetTokenCount > 0;

        public int GetTokenCount { get; private set; }

        protected override Task<string> GetTokenAsync()
        {
            GetTokenCount++;
            return Task.FromResult(Token);
        }
    }
}
