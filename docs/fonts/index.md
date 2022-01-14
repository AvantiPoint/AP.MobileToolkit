!!! Note
    Fonts are handled in a separate [repository](https://github.com/AvantiPoint/AP.MobileToolkit.Fonts).

Currently Font support is targeting Xamarin.Forms and Maui. We are currently planning on adding support for WinUI and Uno Platform.

## Initializing Fonts

### Xamarin.Forms

Fonts should be registered with the FontRegistry when your application is initialized. While this may be in slightly different locations depending on your app model, this should be before you navigate or otherwise create a view that may use the font.

```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        FontRegistry.RegisterFonts(FontAwesomeBrands.Font,
                                   FontAwesomeRegular.Font,
                                   FontAwesomeSolid.Font);
    }
}
```

### Maui

```csharp
public class Startup : IStartup
{
    public void Configure(IAppHostBuilder appBuilder)
    {
        appBuilder
            .UseFormsCompatibility()
            .UseMauiApp<App>()
            .ConfigureIconFonts(b => {
                b.AddFont(FontAwesomeRegular.Font)
                 .AddFont(FontAwesomeSolid.Font);

                // OR
                b.AddFonts(FontAwesomeRegular.Font, FontAwesomeSolid.Font);
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });
    }
}
```

## Using in Prism.Modules
In your module there is no reference to the Toolkit in C#, so the compiler is linking it out and it's not available when it comes time to parse the XAML. Therefore in your Prism.Modules you have to reference it somwehere like this:

```csharp
public MyModule()
    {
        _ = global::AP.MobileToolkit.Fonts.Mappings.FontAwesomeSolid.User;
        _ = global::AP.MobileToolkit.Fonts.FontAwesomeSolid.Font;
        _ = global::AP.MobileToolkit.Xaml.IconExtension.IconNameProperty;
    }
```
    
Of course when if would like to use Brands or Regular you have to switch (or add to) FontAwesomeSolid in the example above according to what you want to use. (For Example _ = global::AP.MobileToolkit.Fonts.FontAwesomeBrands.Font;)

## Additional Resources

- [Font Generator](generator.md)
- [Using Icon Fonts](../forms/fonts/index.md)
