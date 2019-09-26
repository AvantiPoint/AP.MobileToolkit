using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AP.CrossPlatform.Collections;
using AP.MobileToolkit.Mvvm;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ToolkitDemo.Data;
using ToolkitDemo.Models;

namespace ToolkitDemo.ViewModels
{
    public class MainPageViewModel : ReactiveViewModelBase
    {
        private DelegateCommand<Item> _navigateCommand;
        public DelegateCommand<Item> NavigateItemsCommand =>
           _navigateCommand ?? (_navigateCommand = new DelegateCommand<Item>(ExecuteNavigateCommand));

        public ObservableCollection<Grouping<SelectedCategory, Item>> Categories { get; set; }

        private DelegateCommand<Grouping<SelectedCategory, Item>> _headerSelectedCommand;
        public DelegateCommand<Grouping<SelectedCategory, Item>> HeaderSelectedCommand =>
           _headerSelectedCommand ?? (_headerSelectedCommand = new DelegateCommand<Grouping<SelectedCategory, Item>>(g =>
           {
               if (g == null)
                   return;
               g.Key.Selected = !g.Key.Selected;
               if (g.Key.Selected)
               {
                   List<Item> itemsList = DataFactory.DataItems.Where(i => i.Category.CategoryId == g.Key.Category.CategoryId).ToList();
                   foreach (var item in itemsList)
                   {
                       g.Add(item);
                   }
               }
               else
               {
                   g.Clear();
               }
           }));

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
           : base(navigationService, pageDialogService, logger)
        {
            Title = "Toolkit Demo";

            Categories = new ObservableCollection<Grouping<SelectedCategory, Item>>();
            var selectCategories =
                    DataFactory.DataItems.Select(x => new SelectedCategory { Category = x.Category, Selected = false })
                   .GroupBy(sc => new { sc.Category.CategoryId })
                   .Select(g => g.First())
                   .ToList();
            selectCategories.ForEach(sc => Categories.Add(new Grouping<SelectedCategory, Item>(sc, new List<Item>())));
        }

        async void ExecuteNavigateCommand(Item item)
        {
            await NavigationService.NavigateAsync(item.ItemUri);
        }
    }
}
