## CoreServices

The APBaseViewModel is built around maintainability. Part of this includes the use a single provider class that contains various services. The benefit of this pattern is that over time as things change you do not have to change every single ViewModel constructor that inherits from the base ViewModel. As such we also recommend that any base ViewModel that you create or that inherits from our base should follow a similar paradigm for maintainability.

!!! note
    The provider class does not need to be registered witht the container since it is a concrete type.

## Base Properties

- Title: The title is set automatically for you. It will attempt to use `ILocalize` to get the title using the ViewModel Type Name. If it cannot find a value it will remove the ViewModel suffix and Humanize the name. Note that this is set from the `GetTitle()` method and can be overriden to provide custom behavior.
- Subtitle: This isn't directly used but is provided for convienence.
- IsBusy: This uses the protected `ObservableToPropertyHelper<bool> _isBusyHelper` for the Busy Value. By default the base ViewModel does not set the ObservableToPropertyHelper since we cannot possibly know what will cause your specific ViewModel to enter a "Busy" state. This is protected so you can easily set it from your ViewModel's constructor.
- IsNotBusy: Similar to the IsBusy property this utilizes an ObservableToPropertyHelper under the covers. Since this is the exact inverse of IsBusy this is set for you automatically in the base and cannot be overridden.
- NavigateCommand: The NavigateCommand makes uses of the underlying `HandleNavigateRequest` methods and takes a navigation string
- CallbackPath: This can be useful to set in the ViewModel's initialization when you may need to complete a task and then navigate to a specific uri.
- Disposables: The Disposables property is a CompositeDisposable to make it easier to dispose of things automatically using the DisposeWith extension. Anything registered with the CompositeDisposable will be disposed for you automatically when IDestructible is called by Prism. You do not need to ever call `base.Destory();` in the event you have additional cleanup to do in the Destroy method.