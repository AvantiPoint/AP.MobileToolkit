using System.Collections.Generic;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using ToolkitDemo.Models;
using ToolkitDemo.Services;

namespace ToolkitDemo.ViewModels
{
    public class MainPageViewModel : APBaseViewModel
    {
        private IMenuService MenuService { get; }

        public MainPageViewModel(BaseServices baseServices, IMenuService menuService)
           : base(baseServices)
        {
            Title = "Menu";
            MenuService = menuService;
            var categoryList = MenuService.Categories.Select(x => new Grouping<Models.Category, Item>(x.Key, new List<Item>())).ToList();
            Categories = new ObservableRangeCollection<Grouping<Models.Category, Item>>(categoryList);

            NavigationCommand = new DelegateCommand<Item>(OnNavigationCommandExecuted);
            SelectCategoryCommand = new DelegateCommand<Grouping<Models.Category, Item>>(OnSelectCategoryCommandExecuted);
        }

        public IEnumerable<Grouping<Models.Category, Item>> Categories { get; set; }

        public DelegateCommand<Item> NavigationCommand { get; }

        public DelegateCommand<Grouping<Models.Category, Item>> SelectCategoryCommand { get; }

        private async void OnNavigationCommandExecuted(Item item)
        {
            await NavigationService.NavigateAsync($"NavigationPage/{item.Uri}");
        }

        private async void OnSelectCategoryCommandExecuted(Grouping<Models.Category, Item> categoryGroup)
        {
            if (categoryGroup == null)
                return;

            if (categoryGroup.Key.Name.Equals("Home"))
            {
                await NavigationService.NavigateAsync("NavigationPage/HomePage");
                return;
            }

            categoryGroup.Key.IsSelected = !categoryGroup.Key.IsSelected;

            if (categoryGroup.Key.IsSelected)
            {
                var category = MenuService.Categories.FirstOrDefault(x => x.Key.Name == categoryGroup.Key.Name);
                IEnumerable<Item> itemslist = category;
                categoryGroup.AddRange(itemslist);
            }
            else
            {
                categoryGroup.Clear();
            }
        }
    }
}
