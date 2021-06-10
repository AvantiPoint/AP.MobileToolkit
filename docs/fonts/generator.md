The Font Generator will generate the required code for your custom fonts.

!!! Note
    The Font's PostTable does matter. Note that only version 2 contains the name of the Glyph. As a result any font's using other versions in the PostTable should include a mapping css file.

To add a font, create a new project. Typically this will just be a netstandard class library. You do not need any actual code files in the project. Edit the csproj like shown below:

```xml
<ItemGroup>
  <AdditionalFiles Include="fa-regular-400-ttf" 
                   Alias="far" 
                   FontName="FontAwesomeRegular" 
                   Version="5.13.0" 
                   CssFile="fontawesome.min.css" />
</ItemGroup>
```

The Generator will automatically parse both the Font file and the supplied Css or Codepoints file specified. This will generate the required attributes to enabled Font Embedding, as well as the IFont implementation, a static Mapping class that will let you reference `FontAwesomeRegular.User` instead of supplying `far fa-user`. Finally it will generate a second `FontAwesomeRegular` static class in the AP.MobileToolkit.Fonts namespace that provides information about the generated font as well as an instance of the generated IFont instance that you can use to register with the Font Registry.

!!! Warning
    Ligature based fonts are not supported without either a CSS file or Codepoints file like we use for The Google Material Font library.

## Additional Resources

- [Getting Started](index.md)
- [AP.Mobile.Toolkit Fonts for Xamarin.Forms](../forms/fonts/index.md)