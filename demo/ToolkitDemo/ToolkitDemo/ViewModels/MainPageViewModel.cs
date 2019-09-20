using AP.CrossPlatform.Collections;
using Prism.Commands;
using Prism.Navigation;

namespace ToolkitDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand<NavigationItemsListViewModel> _navigateCommand;
        public DelegateCommand<NavigationItemsListViewModel> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<NavigationItemsListViewModel>(ExecuteNavigateCommand));

        public ObservableRangeCollection<NavigationItemsListViewModel> NavigationItemsList { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Toolkit Demo";
            NavigationItemsList = new ObservableRangeCollection<NavigationItemsListViewModel>()
            {
                new NavigationItemsListViewModel { Name = "Home Page", Uri = "NavigationPage/HomePage" },
                new NavigationItemsListViewModel { Name = "Datepicker", Uri = "NavigationPage/BorderlessDatePickerPage" },
                new NavigationItemsListViewModel { Name = "Timepicker", Uri = "NavigationPage/BorderlessTimePickerPage" },
                new NavigationItemsListViewModel { Name = "Borderless Entry", Uri = "NavigationPage/BorderlessEntryPage" },
                new NavigationItemsListViewModel { Name = "Material Datepicker", Uri = "NavigationPage/MaterialDatepickerPage" },
            };
        }

        async void ExecuteNavigateCommand(NavigationItemsListViewModel item)
        {
            await NavigationService.NavigateAsync(item.Uri);
        }
    }
}
