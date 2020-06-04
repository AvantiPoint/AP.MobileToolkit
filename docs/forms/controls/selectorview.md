The SelectorView is built ontop of the RepeaterView so it has a similar API with a few additions that make it great for dealing with Models that implement `ISelectable` from the AP.CrossPlatform.Core.

```csharp
// Note you should implement INotifyPropertyChanged for the properties
public class MyItem : ISelectable
{
    public string Name { get; set; }

    public bool IsSelected { get; set; }
}
```

The SelectorView can be configured to allow you to select only a Single Element or Multiple Elements.