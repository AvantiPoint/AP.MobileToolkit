The BooleanToColorConverter is a great converter for toggling the color on an element based on a boolean value.

!!! note
    The default colors are Green for `true` and Red for `false`.

```xml
<ContentPage xmlns:ap="http://avantipoint.com/mobiletoolkit">
  <ContentPage.Resources>
    <ResourceDictionary>
      <ap:BooleanToColorConverter TrueColor="Blue" FalseColor="Black" x:Key="boolToColor" />
    </ResourceDictionary>
    <BoxView Color="{Binding IsSomethingTrue, Converter={StaticResource boolToColor}}" />
  </ContentPage.Resources>
</ContentPage>
```