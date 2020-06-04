The GravatarImageSource is meant to be an easy to use ImageSource that you can use to easily get the Gravatar Image for any email address.

```xml
<ContentPage>
  <Image>
    <Image.Source>
      <ap:GravatarImageSource Email="{Binding Email}"
                              Size="40"
                              Default="Retro" />
    </Image.Source>
  </Image>
</ContentPage>
```

!!! note
    The GravatarImageSource defaults to a default size of 20 and default gravatar of a MysteryPerson.

There is additionally a XAML extension to make it's use even easier

```xml
<ContentPage>
  <Image Source="{ap:GravatarImageSource Email={Binding Email}}" />
</ContentPage>
```