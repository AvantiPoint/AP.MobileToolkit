# AP.MobileToolkit.Http

The Http package includes a lightweight API Client that makes integrating with a custom backend a breeze. You simply need to provide the base options for where the API Client should send it's requests to along with a callback handler to provie the current Authentication Token.

You can certainly use the trusty old HttpClient directly, however using IApiClient provides a few benefits including:

- Support for Get, Post, Put, Patch, & Delete
- Automatic Retry for:
  - Internal Server Error
  - Service Timeout
  - Service Unavailable
  - Bad Gateway
  - Gateway Timeout
- Control over when to dispose the base HttpClient
- Get helper headers that you can log from your API to track which device your user is using, which verion of the app, and which app, etc, while also being able to easily override or customize the headers you want.

## Configuring the ApiClient

```csharp
public class MyApiClient : ApiClient
{
    private IPushManager _pushManager { get; }
    private IUser _user { get; }

    protected override Uri BaseAddress => new Uri(ConfigurationManager.AppSettings["BackendApi"]);

    public MyApiClient(IUser user, IPushManager pushManager, ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
        : base(logger, appInfo, deviceInfo)
    {
        _pushManager = pushManager;
        _user = user;
    }

    protected override Task<string> GetTokenAsync()
    {
        return Task.FromResult(_user.AccessToken);
    }

    protected override string GetInstallId()
    {
        return _pushManager.CurrentRegistrationToken;
    }
}
```

### Additional Configurations

The ApiClient has a few additional overridable methods that can allow you to further tailor the ApiClient to your specific needs.

#### Default Headers

You can configure the default headers that will be sent by overriding SetDefaultHeaders.

```csharp
protected override void SetDefaultHeaders(HttpClient client)
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
    {
        NoCache = true
    };
    var appName = Regex.Replace(AppInfo.Name, @"\s", string.Empty).ToASCII();
    var agentHeader = ProductHeaderValue.Parse($"{appName}/{AppInfo.VersionString.ToASCII()}");
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(agentHeader));
    client.DefaultRequestHeaders.Add("X-MobileAppVer", AppInfo.VersionString.ToASCII());
    client.DefaultRequestHeaders.Add("X-DeviceModel", DeviceInfo.Model.ToASCII());
    client.DefaultRequestHeaders.Add("X-DeviceManufacturer", DeviceInfo.Manufacturer.ToASCII());
    client.DefaultRequestHeaders.Add("X-DeviceName", DeviceInfo.Name.ToASCII());
    client.DefaultRequestHeaders.Add("X-DevicePlatform", $"{DeviceInfo.Platform}");
    client.DefaultRequestHeaders.Add("X-DeviceIdiom", $"{DeviceInfo.Idiom}");

    if (TryGetInstallId(out var installId))
    {
        client.DefaultRequestHeaders.Add("X-ClientId", installId);
    }
}
```

#### Customizing the Message Handlers

You can optionally provide custom a `HttpMessageHandler` for the HttpClient. Note that you should be intentional on setting the innerHandler to the one passed in otherwise authentication may not work properly.

```csharp
protected override HttpMessageHandler CreateHandler(HttpMessageHandler innerHandler) =>
    new MyCustomHandler(innerHandler);
```

#### Customizing the Authentication Header

There are two ways to configure the Authentication Header. If your authentication relies on using the standard Authentication Header, you can set the AuthenticationScheme in the constructor.

```csharp
public class MyApiClient : ApiClient
{
    public MyApiClient(ILogger logger, IAppInfo appInfo, IDeviceInfo deviceInfo)
        : base(logger, appInfo, deviceInfo)
    {
        AuthenticationScheme = "Basic";
    }
}
```

!!! note
    By default the ApiClient uses a Bearer Authentication Scheme.

In the event that setting the Authentication Scheme is not enough and you actually need a custom header value you should override the `SetAuthenticationHeader` method:

```csharp
protected override void SetAuthenticationHeader(HttpRequestMessage request, string token)
{
    request.Headers.Add("X-MyAuthHeader", token);
}
```

## Using the ApiClient

```csharp
public class FooService
{
    private IApiClient ApiClient { get; }

    [AllowAnnonymous]
    public async Task<bool> GetStatus()
    {
        var result = await ApiClient.GetAsync("api/status");
        return result.IsSuccessStatusCode;
    }

    public async Task DoFoo(SomeModel model)
    {
        await ApiClient.PostAsync("api/doFoo", model);
    }

    public async Task<HttpResponseMessage> DoBar()
    {
        // Dispose HttpClient on each request with ability to reuse the ApiClient
        using (ApiClient)
        {
            return await ApiClient.GetAsync("api/doBar");
        }
    }
}
```

## Additional Headers

Out of the box the ApiClient utilizes IAppInfo and IDeviceInfo from Xamarin.Essentials.Interfaces. These are used in combination with some optional configurations in your ApiClient to add useful headers that can collect metadata about your users.

| Header | Sample Value |
|--------|------------|
| User-Agent | AwesomeApp/2.0.4 |
| X-MobileAppVer | 2.0.4 |
| X-DeviceModel | iPhone10,6 |
| X-DeviceManufacturer | Apple |
| X-DeviceName | Dan's iPhone |
| X-DevicePlatform | iOS |
| X-DeviceIdiom | Phone |
| X-ClientId | {your install Id} |

!!! note
    If you do not want to use IAppInfo and IDeviceInfo to add metadata, you should simply inherit from ApiClientBase to simplify your implementation.
