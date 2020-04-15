using System;
using System.Threading.Tasks;
using AP.MobileToolkit.Http.Tests.Mocks;
using AP.MobileToolkit.Tests.Mocks;
using AP.MobileToolkit.Tests.Tests;
using MockHttpServer;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Http.Tests
{
    public class ApiClientTests : TestBase
    {
        public ApiClientTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task AllowAnnonymousDoesNotInvokeAuthHandler()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                Assert.DoesNotContain("Authorization", req.Headers.AllKeys);
            }))
            {
                var client = CreateClient();

                Assert.False(client.DidGetToken);
                var service = new MockApiService(client);
                await service.MockAnnonymousCall();

                Assert.False(client.DidGetToken);
            }
        }

        [Fact]
        public async Task AuthenticatedCallInvokesAuthHandler()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                foreach (var key in req.Headers.AllKeys)
                    TestOutputHelper.WriteLine(key);
                Assert.Contains("Authorization", req.Headers.AllKeys);
            }))
            {
                var client = CreateClient();

                Assert.False(client.DidGetToken);
                var service = new MockApiService(client);
                await service.MockGet();

                Assert.True(client.DidGetToken);
            }
        }

        [Theory]
        [InlineData(408)]
        [InlineData(500)]
        [InlineData(502)]
        [InlineData(503)]
        [InlineData(504)]
        public async Task RetriesBadRequest(int statusCode)
        {
            int count = 0;
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                if (count++ < 2)
                {
                    rsp.StatusCode = statusCode;
                }
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                var result = await service.MockGet();
                Assert.True(client.DidGetToken);
                Assert.True(result.IsSuccessStatusCode);
                Assert.Equal(1, client.GetTokenCount);
                Assert.Equal(3, count);
            }
        }

        [Fact]
        public async Task IncludesExpectedHeaders()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                var appInfo = new MockAppInfo();
                var deviceInfo = new MockDeviceInfo();
                foreach (var key in req.Headers.AllKeys)
                    TestOutputHelper.WriteLine($"{key}: {req.Headers.Get(key)}");
                Assert.Contains("Authorization", req.Headers.AllKeys);
                Assert.Equal($"BEARER {MockApiClient.Token}", req.Headers.Get("Authorization"));

                Assert.Contains("X-MobileAppVer", req.Headers.AllKeys);
                Assert.Equal(appInfo.VersionString, req.Headers.Get("X-MobileAppVer"));
                Assert.Contains("X-DeviceModel", req.Headers.AllKeys);
                Assert.Equal(deviceInfo.Model, req.Headers["X-DeviceModel"]);
                Assert.Contains("X-DeviceManufacturer", req.Headers.AllKeys);
                Assert.Equal(deviceInfo.Manufacturer, req.Headers["X-DeviceManufacturer"]);
                Assert.Contains("X-DeviceName", req.Headers.AllKeys);
                Assert.Equal(deviceInfo.Name, req.Headers["X-DeviceName"]);
                Assert.Contains("X-DevicePlatform", req.Headers.AllKeys);
                Assert.Equal(deviceInfo.Platform.ToString(), req.Headers["X-DevicePlatform"]);
                Assert.Contains("X-DeviceIdiom", req.Headers.AllKeys);
                Assert.Equal(deviceInfo.Idiom.ToString(), req.Headers["X-DeviceIdiom"]);
                Assert.Contains("User-Agent", req.Headers.AllKeys);
                Assert.Equal($"{appInfo.Name}/{appInfo.VersionString}", req.Headers["User-Agent"]);

                Assert.Contains("Cache-Control", req.Headers.AllKeys);
                Assert.Equal("no-cache", req.Headers["Cache-Control"]);

                Assert.Contains("Accept", req.Headers.AllKeys);
                Assert.Equal("application/json", req.Headers["Accept"]);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockGet();
            }
        }

        [Fact]
        public async Task ServiceSendsRequestWithGetVerb()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("GET", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockGet();
            }
        }

        [Fact]
        public async Task ServiceSendsRequestWithDeleteVerb()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("DELETE", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockDelete();
            }
        }

        [Fact]
        public async Task ServiceSendsRequestWithPatchVerb()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("PATCH", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockPatch();
            }
        }

        [Fact]
        public async Task ServiceSendsRequestWithPostVerb()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("POST", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockPost();
            }
        }

        [Fact]
        public async Task ServiceAllowMultipleDisposible()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("GET", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockDisposibleCall();
                await service.MockDisposibleCall();
                await service.MockDisposibleCall();
            }
        }

        [Fact]
        public async Task ServiceSendsRequestWithPutVerb()
        {
            using (new MockServer(MockApiClientOptions.TestPort, string.Empty, (req, rsp, prm) =>
            {
                TestOutputHelper.WriteLine($"HttpMethod: {req.HttpMethod}");
                Assert.Equal("PUT", req.HttpMethod);
            }))
            {
                var client = CreateClient();
                var service = new MockApiService(client);
                await service.MockPut();
            }
        }

        [Fact]
        public async Task GetsUserFromToken()
        {
            var client = CreateClient();
            var user = await client.GetUserAsync();
            Assert.True(client.DidGetToken);
            Assert.NotNull(user);
            Assert.Equal("c652decb-374e-4a34-b6e0-c0d377436a71", user.Id);
            Assert.Equal("John", user.FirstName);
            Assert.Equal("Doe", user.LastName);
            Assert.Equal(new DateTime(2018, 1, 18, 1, 30, 22), user.IssuedAt);
        }

        private MockApiClient CreateClient()
        {
            return new MockApiClient(new MockApiClientOptions(), new XunitLogger(TestOutputHelper), new MockAppInfo(), new MockDeviceInfo());
        }
    }
}
