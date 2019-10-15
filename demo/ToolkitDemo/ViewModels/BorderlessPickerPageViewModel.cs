using System.Collections.Generic;
using AP.CrossPlatform.Collections;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class BorderlessPickerPageViewModel : DemoPageViewModelBase
    {
        public BorderlessPickerPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            Options = new ObservableRangeCollection<string>()
            {
                "Option 1",
                "Option 2",
                "Option 3",
                "Option 4",
            };
        }

        public IEnumerable<string> Options { get; }
    }
}
