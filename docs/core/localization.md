To really understand the power of ILocalize we'll consider for a moment the scenario that you may want to use the localized resources that are part of the Toolkit and your own resources.

```csharp
ILocalize localize = new ResxLocalize();

// Register local resources
localize.RegisterManager(Resources.ResourceManager);

// Register Toolkit resources
localize.RegisterManager(ToolkitResources.ResourceManager);
```

Now that our Resource Manager's are registered we can easily retrieve a localized value:

```csharp
Title = localize["ViewATitle"];
```

!!! note
    The Localization service will iterate through the ResourceManager's that have been registered and provide the value from the first ResourceManager that contains the key.

!!! important
    The ResxLocalize service is meant to be a singleton. It can technically be accessed by calling `ResxLocalize.Current`. It is generally recommended that you use a DI Container to register and resolve the service.

## Localized Culture

By default we will use the CultureInfo from the current UI Culture. In the event that you want to override this behavior for instance if you want to make it user selectable you can set the culture and all subsequent requests will use the specified culture.

```csharp
// If you don't want to use the current UI Culture
localize.SetCulture(new CultureInfo("es");
```

## Debugging

By default if no key is found it will return an empty string. To debug this you may want to set the Debug property when your app is initializing to instead return a debug value:

```csharp
#if DEBUG
ResxLocalize.Debug = true;
#endif
var localize = new ResxLocalize();
var value = localize["NonExistentKey"];
```

In this example we would expect to get an output of `MISSING NonExistentKey`.

## XAML Extensions

If you are using a platform specific package like `AP.MobileToolkit.Forms` you can expect to find a XAML extension that makes use of ILocalize which can be used like:

```xml
<ContentPage xmlns:ap="http://avantipoint.com/mobiletoolkit">
  <Label Text="{ap:Localize 'SomeKey'}" />
</ContentPage>
```
