# AvantiPoint Mobile Toolkit

The AvantiPoint Mobile Toolkit is built for producing Enterprise Quality applications. The Toolkit is broken into smaller chunks that are largely platform independent. Currently only the AP.MobileToolkit package has any dependency on Xamarin.Forms.

[![Build Status](https://dev.azure.com/avantipoint/CrossPlatform/_apis/build/status/AvantiPoint.AP.MobileToolkit?branchName=master)](https://dev.azure.com/avantipoint/CrossPlatform/_build/latest?definitionId=55?branchName=master)

## NuGet

You can add the MyGet CI feed to nuget by adding it as a source in Visual Studio:

`https://www.myget.org/F/apmobiletoolkit/api/v3/index.json`

| Package | NuGet | MyGet |
|-------|:-----:|:------:|
| AP.CrossPlatform.Auth | [![CrossPlatAuthShield]][CrossPlatAuthNuGet] | [![CrossPlatAuthMyGetShield]][CrossPlatAuthMyGet] |
| AP.CrossPlatform.Core | [![CrossPlatCoreShield]][CrossPlatCoreNuGet] | [![CrossPlatCoreMyGetShield]][CrossPlatCoreMyGet] |
| AP.MobileToolkit | [![MobileToolkitShield]][MobileToolkitNuGet] | [![MobileToolkitMyGetShield]][MobileToolkitMyGet] |
| AP.MobileToolkit.AAD | [![MobileToolkitAADShield]][MobileToolkitAADNuGet] | [![MobileToolkitAADMyGetShield]][MobileToolkitAADMyGet] |
| AP.MobileToolkit.Http | [![MobileToolkitHttpShield]][MobileToolkitHttpNuGet] | [![MobileToolkitHttpMyGetShield]][MobileToolkitHttpMyGet] |
| AP.MobileToolkit.Modularity | [![MobileToolkitModularityShield]][MobileToolkitModularityNuGet] | [![MobileToolkitModularityMyGetShield]][MobileToolkitModularityMyGet] |

## AP.MobileToolkit

The MobileToolkit package has a number of Xamarin.Forms specific helpers including, Behaviors, Controls, Effects, and Base ViewModels for Prism Applications. This includes:

- ViewModelBase - which inherits from Prism's BindableBase
- ReactiveViewModelBase - which inherits from ReactiveUI's ReactiveObject

Both base ViewModel's implement all of the important Prism Navigation Interfaces. This allows you to rapidly develop application by only adding what you need while taking advantage of a Base Navigation Command / Handler method and support for IsBusy/IsNotBusy.

### Controls

| Control | Android | iOS | UWP |
|---------|:-------:|:---:|:---:|
| BadgeView | X | X | X |
| BorderlessDatePicker | X | X | X |
| BorderlessEditor | X | X | X |
| BorderlessEntry | X | X | X |
| BorderlessPicker | X | X | X |
| BorderlessTimePicker | X | X | X |
| CustomRadioButton | X | X | - |
| DatePickerCell | X | X | X |
| GravatarImageSource | X | X | X |
| IconImageSource | X | X | X |
| IconSpan | X | X | X |
| ImageEntry | X | X | - |
| MarkdownTextView | X | X | - |
| MaterialButton | X | X| X |
| MaterialDatePicker | X | X| X |
| MaterialEntry | X | X| X |
| MaterialPicker | X | X| X |
| MaterialTimePicker | X | X| X |
| RadioButtonGroup | X | X| - |
| RepeaterView | X | X | X |
| SelectorView | X | X | X |
| Separator | X | X | - |
| SwipeCardView | X | X | - |

## AP.MobileToolkit.Http

The Http package includes a lightweight API Client that makes integrating with a custom backend a breeze. You simply need to provide the base options for where the API Client should send it's requests to along with a callback handler to provie the current Authentication Token.

```cs
public class MyAppApiClientOptions : IApiClientOptions
{
    public string InstallId => AppCenter.InstallId;

    public string BaseUri => ConfigurationManager.AppSettings["BackendApi"];
}

public class AuthenticationHandler : IAuthenticationHandler
{
    private IServiceProvider _serviceProvider { get; }

    public AuthenticationHandler(IServiceProvider serviceProvicer)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<string> GetTokenAsync()
    {
        var user = (IUser)_serviceProvider.GetService(typeof(IUser));
        return Task.FromResult(user.AccessToken);
    }
}
```

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

```cs
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

[CrossPlatAuthNuGet]: https://www.nuget.org/packages/AP.CrossPlatform.Auth
[CrossPlatAuthShield]: https://img.shields.io/nuget/vpre/AP.CrossPlatform.Auth.svg
[CrossPlatAuthMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.CrossPlatform.Auth
[CrossPlatAuthMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.CrossPlatform.Auth.svg

[CrossPlatCoreNuGet]: https://www.nuget.org/packages/AP.CrossPlatform.Core
[CrossPlatCoreShield]: https://img.shields.io/nuget/vpre/AP.CrossPlatform.Core.svg
[CrossPlatCoreMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.CrossPlatform.Core
[CrossPlatCoreMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.CrossPlatform.Core.svg

[MobileToolkitNuGet]: https://www.nuget.org/packages/AP.MobileToolkit
[MobileToolkitShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.svg
[MobileToolkitMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit
[MobileToolkitMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.svg

[MobileToolkitAADNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.AAD
[MobileToolkitAADShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.AAD.svg
[MobileToolkitAADMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.AAD
[MobileToolkitAADMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.AAD.svg

[MobileToolkitHttpNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.Http
[MobileToolkitHttpShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.Http.svg
[MobileToolkitHttpMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.Http
[MobileToolkitHttpMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.Http.svg

[MobileToolkitModularityNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.Modularity
[MobileToolkitModularityShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.Modularity.svg
[MobileToolkitModularityMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.Modularity
[MobileToolkitModularityMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.Modularity.svg