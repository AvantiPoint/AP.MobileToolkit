# Using The Fonts

You may be asking why use the MobileToolkit for your Icon Fonts? Xamarin.Forms already has a solution for Fonts right? In short, an Icon Font isn't really any different than any other font. If you provide the unicode character and the font family it will just work, and the FontImageSource will work for those fonts. The problem with this approach is that it's largely meaningless and means you're setting both a Text and Font Family property. The MobileToolkit helps you to write code that is easier to read and is ultimately self documenting by taking the unicode character out of the equation and letting you use libraries like Font Awesome the same way you've always used them on the web by just referencing the css classes like `fas fa-users`.

## Getting Started

Like the rest of the Toolkit we try to make development as easy as possible. Why have to spend extra time writing code comments or ending up with code that's meaningless unless you spend the time to lookup what the unicode character is.

```xml
<!-- fas fa-users -->
<Label Text="\uf0c0"
       FontFamily="fa-solid-900.ttf" />
```

While you could try to specify the unicode character ultimately this just is hard to read, and requires some sort of comment in order for you to come along later an know what icon just got used. This is where the MobileToolkit Fonts comes in to make things easier. After [registering](index.md) your fonts, you can use them anywhere in your app similar to how you might use it on the web like `fas fa-users`.

For those using older versions of the MobileToolkit you may be used to some Xaml Extensions like the following:

```xml
<Label Text="{ap:Icon 'fas fa-users'}" />
```

While the XamlExtension isn't going anywhere, we wanted to give people a cleaner approach which we recommend that you migrate to, or start out with if you're brand new to the Toolkit. In version 4 we introduced the FontIcon class to help you like:

```xml
<Label ap:FontIcon.Icon="fas fa-users" />
<Button ap:FontIcon.Icon="fas fa-users" />
```

### Extending the FontIcon functionality

There may be times where you are working with a custom control from a 3rd party vendor such as Syncfusion or Telerik where you may want to extend functionality to work with one of their controls in your app. Since the MobileToolkit cannot handle every single control we do provide a hook for you to be able to pass a delegate to enable support for these additional controls.

```csharp
public partial class App : Application
{
    public App()
    {
        FontIcon.RegisterDefaultOnChangedHandler(FontIconHandler);
    }

    private void FontIconHandler(BindableObject bindable, string selector, string glyph, string fontFamily)
    {
        switch(bindable)
        {
            case MyCustomControl mcc:
                mcc.Text = glyph;
                mcc.FontFamily = fontFamily;
                break;
            case MyOtherCustomControl mocc:
                mocc.IconImageSource = FontIcon.CreateIconImageSource(bindable, selector);
        }
    }
}
```

## Mapping Class

The [Font Generator](generator.md) will additionally generate a Mapping class. If you are in XAML this is already part of the MobileToolkit namespace. Some people don't want a "magic string" that could be subject to typos. The mapping class is designed to help you with this by providing you a compiler friendly class you can reference like:

```xml
<Label ap:FontIcon.Icon="{x:Static ap:FontAwesomeSolid.Users}" />
```

## Image Source

The Mobile Toolkit provides an IconImageSource to make it easy to use Icon Fonts for a whole host of scenarios such as menu bar icons. While you can use the FontIcon class create the icon the same way you would on a Label or Button, may want to just simply set the Image Source directly which you can do either with a more verbose ImageSource syntax or XAML Extension as shown here.

```xml
<Image Source={ap:IconImageSource 'fas fa-users', Color=Black, Size=16} />

<Image>
  <Image.Source>
    <IconImageSource Name="fas fa-users"
                     Color="Black"
                     Size="16" />
  </Image.Source>
</Image>
```

## Legacy

The Toolkit includes an `Icon` XAML Extension. This can be used by passing in a string with the font alias and glyph name. For instance you might use `far fa-user` to show the User icon from Font Awesome Regular. Alternatively you can use the Static Mapping class to strongly type the icons like `FontAwesomeRegular.User`.

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:ap="http://avantipoint.com/mobiletoolkit"
             x:Class="SampleFonts.MainPage">
  <StackLayout>
    <Label Text="{ap:Icon 'far fa-user'}" />
    <Button Text="{ap:Icon 'far fa-user'}" />
    <Image Source="{FontImage Glyph={ap:Icon 'far fa-check-circle'}, Color=Blue, Size=60}" />
    <Label Text="{ap:Icon {x:Static ap:FontAwesomeRegular.User}}" />
  </StackLayout>
</ContentPage>
```

!!! NOTE
    While this is considered legacy, the icon extension still works and we currently do not plan on removing it as this would break anyone updating.

## Additional Resources

- [Getting Started](../../fonts/index.md)
- [Font Generation](../../fonts/generator.md)