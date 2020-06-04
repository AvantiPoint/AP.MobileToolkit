!!! Note
    Fonts are handled in a separate [repository](https://github.com/AvantiPoint/AP.MobileToolkit.Fonts).

Currently Font support is targeting Xamarin.Forms and is planned for WinUI and Uno Platform.

## Initializing Fonts

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

## Additional Resources

- [Font Generator](generator.md)
- [AP.Mobile.Toolkit Fonts for Xamarin.Forms](../forms/fonts/index.md)