using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class MainPageViewModel : ReactiveViewModelBase
    {
        private DelegateCommand<NavigationItemsList> _navigateCommand;
        public DelegateCommand<NavigationItemsList> NavigateItemsCommand =>
           _navigateCommand ?? (_navigateCommand = new DelegateCommand<NavigationItemsList>(ExecuteNavigateCommand));

        public ObservableRangeCollection<NavigationItemsList> NavigationItemsList { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
           : base(navigationService, pageDialogService, logger)
        {
            Title = "Toolkit Demo";
            NavigationItemsList = new ObservableRangeCollection<NavigationItemsList>()
            {
                new NavigationItemsList { Name = "Home Page", Uri = "NavigationPage/HomePage" },
                new NavigationItemsList { Name = "Datepicker", Uri = "NavigationPage/BorderlessDatePickerPage" },
                new NavigationItemsList { Name = "Timepicker", Uri = "NavigationPage/BorderlessTimePickerPage" },
                new NavigationItemsList { Name = "Borderless Entry", Uri = "NavigationPage/BorderlessEntryPage" },
                new NavigationItemsList { Name = "Material Datepicker", Uri = "NavigationPage/MaterialDatepickerPage" },
            };
        }

        async void ExecuteNavigateCommand(NavigationItemsList item)
        {
            await NavigationService.NavigateAsync(item.Uri);
        }
    }
}
