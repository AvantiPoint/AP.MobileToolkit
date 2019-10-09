using System.Collections.Generic;
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

        public IEnumerable<Grouping<Models.Category, Item>> Categories { get; set; }

        public DelegateCommand<Item> NavigateItemsCommand { get; }

        public DelegateCommand<Grouping<Models.Category, Item>> HeaderSelectedCommand { get; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, IMenu menu)
           : base(navigationService, pageDialogService, logger)
        {
            Title = "Menu";
            Menu = menu;
            Categories = Menu.Categories;

            NavigateItemsCommand = new DelegateCommand<Item>(ExecuteNavigateCommand);
            HeaderSelectedCommand = new DelegateCommand<Grouping<Models.Category, Item>>(CategorySelectedCommand);
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
