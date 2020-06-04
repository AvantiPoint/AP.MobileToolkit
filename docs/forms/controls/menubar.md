The MenuBar is a control purpose built for the [Menu Builder](../menus/index.md) to make it easier to add menus, typically to the Master of a MasterDetailPage.

```xml
<MasterDetailPage>
  <MasterDetailPage.Master>
    <ContentPage Title="Menu">
      <ap:MenuBar MenuOptions="{Binding MenuOptions}"
                  NavigationCommand="{Binding NavigationCommand}" />
    </ContentPage>
  </MasterDetailpage.Master>
</MasterDetailPage>
```

## Additional Resources

- [Menu Builder](../menus/index.md)