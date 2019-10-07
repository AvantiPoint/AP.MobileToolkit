using System.Collections.ObjectModel;
using AP.CrossPlatform.Collections;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ToolkitDemo.Models;
using ToolkitDemo.SideMenu;

namespace ToolkitDemo.ViewModels
{
    public class RadioButtonPageViewModel : DemoPageViewModelBase
    {
        public IMenu Menu { get; set; }
        public ObservableCollection<string> RadioItems { get; set; }
        public RadioButtonPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, IMenu menu)
            : base(navigationService, pageDialogService, logger)
        {
            RadioItems = new ObservableCollection<string>()
            {
                "Radio Option 1",
                "Radio Option 2",
                "Radio Option 3"
            };
        }
    }
}
