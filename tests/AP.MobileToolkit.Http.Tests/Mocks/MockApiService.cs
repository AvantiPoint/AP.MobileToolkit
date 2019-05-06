using System.Net.Http;
using System.Threading.Tasks;
using AP.MobileToolkit.Http;

namespace AP.MobileToolkit.Http.Tests.Mocks
{
    public class MockApiService
    {
        private IApiClient _apiClient { get; }

        public MockApiService(IApiClient apiClient) => _apiClient = apiClient;

        [AllowAnonymous]
        public Task<HttpResponseMessage> MockAnnonymousCall() =>
            _apiClient.GetAsync("");

        public Task<HttpResponseMessage> MockDelete() =>
            _apiClient.DeleteAsync("?id=1", null);

        public Task<HttpResponseMessage> MockGet() =>
            _apiClient.GetAsync("");

        public Task<HttpResponseMessage> MockPost() =>
            _apiClient.PostAsync("", null);

        public Task<HttpResponseMessage> MockPut() =>
            _apiClient.PutAsync("", null);
    }
}
