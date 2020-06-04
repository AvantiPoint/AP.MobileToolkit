It's a pretty common paradigm that you may have a MasterDetailPage that may need to update with various menu options. The abstractions for the Menu Builder help you do just that along with the [MenuBar](../controls/menubar.md).

!!! note
    This could be used either with Prism's INavigationService or Shell Navigation

```csharp
IMenuBuilder builder = new MenuBuilder();
builder.RegisterOption(new MainMenuOption
{
    Text = "Home",
    Uri = "/home",
    Priority = -1,

    // far fa-dashboard
    Glyph = Mappings.FontAwesomeRegular.Dashboard
});
```

!!! note
    If you are using the [AP.MobileToolkit.Fonts](../fonts/index.md) you only need to provide the Mapping value for the glyph. Otherwise you should use the Unicode value and Font Family.

## Additional Resources

- [MenuBar](../controls/menubar.md)