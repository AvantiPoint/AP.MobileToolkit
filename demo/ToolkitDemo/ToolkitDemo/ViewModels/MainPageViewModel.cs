using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ToolkitDemo.Models;
using ToolkitDemo.SideMenu;

namespace ToolkitDemo.ViewModels
{
    public class MainPageViewModel : ReactiveViewModelBase
    {
        private IMenu Menu { get; set; }

        private DelegateCommand<Item> _navigateCommand;
        public DelegateCommand<Item> NavigateItemsCommand =>
           _navigateCommand ?? (_navigateCommand = new DelegateCommand<Item>(ExecuteNavigateCommand));

        public ObservableCollection<Grouping<Models.Category, Item>> Categories { get; set; }

        private DelegateCommand<Grouping<Models.Category, Item>> _headerSelectedCommand;
        public DelegateCommand<Grouping<Models.Category, Item>> HeaderSelectedCommand =>
           _headerSelectedCommand ?? (_headerSelectedCommand = new DelegateCommand<Grouping<Models.Category, Item>>(CategorySelectedCommand));

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, IMenu menu)
           : base(navigationService, pageDialogService, logger)
        {
            Title = "Menu";
            Menu = menu;
            Categories = Menu.Categories;
        }

        private async void ExecuteNavigateCommand(Item item)
        {
            await NavigationService.NavigateAsync(item.ItemUri);
        }

        private async void CategorySelectedCommand(Grouping<Models.Category, Item> categoryGroup)
        {
            if (categoryGroup == null)
                return;

            if (categoryGroup.Key.CategoryTitle.Equals("Home"))
            {
                await NavigationService.NavigateAsync("NavigationPage/HomePage");
                categoryGroup.Clear();
                return;
            }

            categoryGroup.Key.IsSelected = !categoryGroup.Key.IsSelected;
            if (categoryGroup.Key.IsSelected)
            {
                List<Item> itemslist = Menu.MenuItems.Where(i => i.CategoryId == categoryGroup.Key.CategoryId).ToList();
                foreach (var item in itemslist)
                {
                    categoryGroup.Add(item);
                }
            }
            else
            {
                categoryGroup.Clear();
            }
        }
    }
}
