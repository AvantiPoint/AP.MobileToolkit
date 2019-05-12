using System.Net.Http;
using System.Threading.Tasks;
using AP.MobileToolkit.Http;

namespace AP.MobileToolkit.Http.Tests.Mocks
{
    public class MockApiService
    {
        private IApiClient ApiClient { get; }

        public MockApiService(IApiClient apiClient) => ApiClient = apiClient;

        [AllowAnonymous]
        public Task<HttpResponseMessage> MockAnnonymousCall() =>
            ApiClient.GetAsync(string.Empty);

        public Task<HttpResponseMessage> MockDelete() =>
            ApiClient.DeleteAsync("?id=1", null);

        public Task<HttpResponseMessage> MockGet() =>
            ApiClient.GetAsync(string.Empty);

        public Task<HttpResponseMessage> MockPost() =>
            ApiClient.PostAsync(string.Empty, null);

        public Task<HttpResponseMessage> MockPut() =>
            ApiClient.PutAsync(string.Empty, null);
    }
}
