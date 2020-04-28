using AP.MobileToolkit.Mvvm;

namespace ToolkitDemo.ViewModels
{
    public class UppercaseConverterPageViewModel : DemoPageViewModelBase
    {
        public UppercaseConverterPageViewModel(BaseServices baseServices)
            : base(baseServices)
        {
            TextToBeConverted = "Text to be converted to uppercase";
        }

        public string TextToBeConverted { get; }
    }
}
