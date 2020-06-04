The Font Generator will generate the required code for your custom fonts.

!!! Note
    The Font's PostTable does matter. Note that only version 2 contains the name of the Glyph. As a result any font's using other versions in the PostTable should include a mapping css file.

To add a font, create a new project. Typically this will just be a netstandard class library, but this could be a multi-targeted project. You should not have any actual code files in the project. Edit the csproj like shown below:

```xml
<ItemGroup>
  <EmbeddedFont Include="fa-regular-400.ttf"
                Alias="far"
                FontName="FontAwesomeRegular"
                Version="5.13.0" />
</ItemGroup>
```

We will add an EmbeddedFont with the path to the otf or ttf font file. Be sure to add the value for the Alias. This is what allows us to do something like `far fa-user`. Add the FontNamme. This will control the class name used for the generated Font Definition and static mapping class.

!!! Warning
    The Google Material Font is not currently supported as this uses ligatures to make up an icon by spelling it out (i.e. GitHub, Microsoft). For this reason there is currently no way for the Font Generator to understand what icons/glyphs actually belong as part of the font or what the proper name of the glyph would be.

## Additional Resources

- [Getting Started](index.md)
- [AP.Mobile.Toolkit Fonts for Xamarin.Forms](../forms/fonts/index.md)