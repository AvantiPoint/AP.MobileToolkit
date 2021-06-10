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

## Additional Resources

- [Font Generator](generator.md)
- [Using Icon Fonts](../forms/fonts/index.md)