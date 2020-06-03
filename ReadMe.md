# AvantiPoint Mobile Toolkit

The AvantiPoint Mobile Toolkit is built for producing Enterprise Quality applications. The Toolkit is broken into smaller chunks that are largely platform independent.

[![Build Status](https://dev.azure.com/avantipoint/AP.MobileToolkit/_apis/build/status/AvantiPoint.AP.MobileToolkit?branchName=master&stageName=Build)](https://dev.azure.com/avantipoint/AP.MobileToolkit/_build/latest?definitionId=66&branchName=master)

## NuGet

You can add the MyGet CI feed to nuget by adding it as a source in Visual Studio:

`https://www.myget.org/F/apmobiletoolkit/api/v3/index.json`

#### Cross Platform

| Package | NuGet | MyGet |
|-------|:-----:|:------:|
| AP.CrossPlatform.Auth | [![CrossPlatAuthShield]][CrossPlatAuthNuGet] | [![CrossPlatAuthMyGetShield]][CrossPlatAuthMyGet] |
| AP.CrossPlatform.Core | [![CrossPlatCoreShield]][CrossPlatCoreNuGet] | [![CrossPlatCoreMyGetShield]][CrossPlatCoreMyGet] |
| AP.MobileToolkit.AAD | [![MobileToolkitAADShield]][MobileToolkitAADNuGet] | [![MobileToolkitAADMyGetShield]][MobileToolkitAADMyGet] |
| AP.MobileToolkit.Http | [![MobileToolkitHttpShield]][MobileToolkitHttpNuGet] | [![MobileToolkitHttpMyGetShield]][MobileToolkitHttpMyGet] |
| AP.MobileToolkit.Modularity | [![MobileToolkitModularityShield]][MobileToolkitModularityNuGet] | [![MobileToolkitModularityMyGetShield]][MobileToolkitModularityMyGet] |
| AP.MobileToolkit.Resources | [![MobileToolkitResourcesShield]][MobileToolkitResourcesNuGet] | [![MobileToolkitResourcesMyGetShield]][MobileToolkitResourcesMyGet] |
| AP.MobileToolkit.RxUI | [![MobileToolkitRxUIShield]][MobileToolkitRxUINuGet] | [![MobileToolkitRxUIMyGetShield]][MobileToolkitRxUIMyGet] |

#### Xamarin.Forms

| Package | NuGet | MyGet |
|-------|:-----:|:------:|
| AP.MobileToolkit.Forms | [![MobileToolkitFormsShield]][MobileToolkitFormsNuGet] | [![MobileToolkitFormsMyGetShield]][MobileToolkitFormsMyGet] |
| AP.MobileToolkit.Forms.Mvvm | [![MobileToolkitFormsMvvmShield]][MobileToolkitFormsMvvmNuGet] | [![MobileToolkitFormsMvvmMyGetShield]][MobileToolkitFormsMvvmMyGet] |
<!--
| AP.MobileToolkit | [![MobileToolkitShield]][MobileToolkitNuGet] | [![MobileToolkitMyGetShield]][MobileToolkitMyGet] |
-->

## AP.MobileToolkit

The MobileToolkit package has a number of Xamarin.Forms specific helpers including, Behaviors, Controls, Effects, and Base ViewModels for Prism Applications. This includes:

- ViewModelBase - which inherits from Prism's BindableBase
- ReactiveViewModelBase - which inherits from ReactiveUI's ReactiveObject

Both base ViewModel's implement all of the important Prism Navigation Interfaces. This allows you to rapidly develop application by only adding what you need while taking advantage of a Base Navigation Command / Handler method and support for IsBusy/IsNotBusy.

### Controls

| Control | Android | iOS | UWP |
|---------|:-------:|:---:|:---:|
| BadgeView | X | X | X |
| CustomRadioButton | X | X | - |
| DatePickerCell | X | X | X |
| GravatarImageSource | X | X | X |
| IconImageSource | X | X | X |
| IconSpan | X | X | X |
| ImageEntry | X | X | - |
| MarkdownTextView | X | X | - |
| RadioButtonGroup | X | X| - |
| RepeaterView | X | X | X |
| SelectorView | X | X | X |
| Separator | X | X | - |
| SwipeCardView | X | X | - |



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

[MobileToolkitResourcesNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.Resources
[MobileToolkitResourcesShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.Resources.svg
[MobileToolkitResourcesMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.Resources
[MobileToolkitResourcesMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.Resources.svg

[MobileToolkitRxUINuGet]: https://www.nuget.org/packages/AP.MobileToolkit.RxUI
[MobileToolkitRxUIShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.RxUI.svg
[MobileToolkitRxUIMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.RxUI
[MobileToolkitRxUIMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.RxUI.svg

<!-- Xamarin.Forms -->
[MobileToolkitFormsNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.Forms
[MobileToolkitFormsShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.Forms.svg
[MobileToolkitFormsMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.Forms
[MobileToolkitFormsMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.Forms.svg

[MobileToolkitFormsMvvmNuGet]: https://www.nuget.org/packages/AP.MobileToolkit.Forms.Mvvm
[MobileToolkitFormsMvvmShield]: https://img.shields.io/nuget/vpre/AP.MobileToolkit.Forms.Mvvm.svg
[MobileToolkitFormsMvvmMyGet]: https://www.myget.org/feed/apmobiletoolkit/package/nuget/AP.MobileToolkit.Forms.Mvvm
[MobileToolkitFormsMvvmMyGetShield]: https://img.shields.io/myget/apmobiletoolkit/vpre/AP.MobileToolkit.Forms.Mvvm.svg