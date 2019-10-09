using System.Collections.Generic;
using System.Linq;
using AP.CrossPlatform.Collections;
using ToolkitDemo.Extensions;
using ToolkitDemo.Helpers;
using ToolkitDemo.Models;

namespace ToolkitDemo.Services
{
    public class MenuService : IMenuService
    {
        public IEnumerable<Grouping<Category, Item>> Categories { get; set; }

        public MenuService()
        {
            object[] menuItemAttributes = GetType().Assembly.GetCustomAttributes(typeof(MenuItemAttribute), false);
            var categoryList = new List<Grouping<Category, Item>>();

            categoryList = menuItemAttributes.OrderBy(i => (i as MenuItemAttribute).MenuGroup).GroupBy(x => (x as MenuItemAttribute).MenuGroup)
                .Select(g => new Grouping<Category, Item>(new Category() { Name = g.Key.Description() }, g.Select(x => new Item()
                {
                    Name = (x as MenuItemAttribute).DisplayName,
                    Uri = (x as MenuItemAttribute).NavigationName
                }).OrderBy(x => x.Name).ToList()))
                .ToList();

            // Adding default menu item for Home at 0 index
            categoryList.Insert(0, new Grouping<Category, Item>(new Category() { Name = "Home" }, new List<Item>()));

            Categories = new ObservableRangeCollection<Grouping<Category, Item>>(categoryList);
        }
    }
}
