Why provide a RepeaterView? After all with BindableLayout in Xamarin.Forms you can now pretty much accomplish the same thing right?

Technically yes you can, however the RepeaterView in the Toolkit simplifies the process by providing the ItemsSource and ItemTemplate properties as part of the layout. Additionally it provides a Header property that can be used.

```xml
<ap:RepeaterView ItemsSource="{Binding MyItems}"
                 ItemTemplate="{StaticResource MyDataTemplate}">
  <ap:RepeaterView.Header>
    <Label Text="Example Header" />
  </ap:RepeaterView.Header>
</ap:RepeaterView>
```