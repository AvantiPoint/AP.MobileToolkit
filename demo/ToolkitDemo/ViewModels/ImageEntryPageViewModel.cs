using System.Collections.Generic;
using System.Collections.ObjectModel;
using AP.CrossPlatform.Collections;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ToolkitDemo.Models;
using ToolkitDemo.SideMenu;

namespace ToolkitDemo.ViewModels
{
    public class ImageEntryPageViewModel : DemoPageViewModelBase
    {
        public ObservableCollection<Grouping<Models.Category, Item>> Categories { get; set; }
        public ObservableCollection<Item> Items;

        public IMenu Menu { get; set; }
        public ImageEntryPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger, IMenu menu)
            : base(navigationService, pageDialogService, logger)
        {
            Menu = menu;
            Categories = Menu.Categories;

            Items = new ObservableCollection<Item>();
            foreach (var item in Menu.MenuItems)
            {
                Items.Add(item);
            }
        }
    }
}
