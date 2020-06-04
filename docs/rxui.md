# AP.MobileToolkit.RxUI

### Cron Jobs

The RxUI package is a lightweight helper. This provides us an `ObservableCron` that allows us to create an observable pipe based on a Cron scheduler.

```csharp
ObservableCron.Start("5 * * * *")
              .InvokeCommand(SomeCommand);
```

### Prism Support

Also included is a the ReactiveUISupportModule. This module utilizes the `ILogger` from `Prism.Plugin.Logging` to log exceptions handled by the RxApp.DefaultExceptionHandler.

```csharp
protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
{
    moduleCatalog.AddModule<ReactiveUISupportModule>();
}
```

What does this do?

```csharp
public ReactiveUISupportModule(ILogger logger)
{
    Logger = logger;
    RxApp.DefaultExceptionHandler = this;
}

public void OnError(Exception error)
{
    Logger.Report(error, new Dictionary<string, string>
    {
        { "Source", "Unobserved ReactiveUI Exception 'OnError'" }
    });
}

public void OnNext(Exception value)
{
    Logger.Report(value, new Dictionary<string, string>
    {
        { "Source", "Unobserved ReactiveUI Exception 'OnNext'" }
    });
}
```
