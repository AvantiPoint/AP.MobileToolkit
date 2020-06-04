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

## Additional Resources

- [Getting Started](../../fonts/index.md)
- [Font Generation](../../fonts/generator.md)