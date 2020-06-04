The APBaseDialogViewModel implements both IDialogAware and IDestructible. This allows you to write less code and inherits from ReactiveObject so it's ready for Reactive programmming out of the box.

```csharp
public class MyDialog : APBaseDialogViewModel
{
    [Reactive]
    public string Title { get; set; }

    [Reactive]
    public string Message { get; set; }
}
```