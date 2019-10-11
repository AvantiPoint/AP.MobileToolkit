using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using AP.CrossPlatform.Collections;
using AP.CrossPlatform.Extensions;
using ToolkitDemo.Helpers;
using ToolkitDemo.Models;

namespace ToolkitDemo.Services
{
    public class MenuService : IMenuService
    {
        public MenuService()
        {
            IEnumerable<MenuItemAttribute> menuItemAttributes = GetType().Assembly.GetCustomAttributes<MenuItemAttribute>();
            var categoryList = new List<Grouping<Category, Item>>();

            categoryList = menuItemAttributes.OrderBy(i => i.MenuGroup).GroupBy(x => x.MenuGroup)
                .Select(g => new Grouping<Category, Item>(new Category() { Name = g.Key.GetAttribute<DescriptionAttribute>().Description }, g.Select(x => new Item()
                {
                    Name = x.DisplayName,
                    Uri = x.NavigationName
                }).OrderBy(x => x.Name).ToList()))
                .ToList();

            // Adding default menu item for Home at 0 index
            categoryList.Insert(0, new Grouping<Category, Item>(new Category() { Name = "Home" }, new List<Item>()));

            Categories = new ObservableRangeCollection<Grouping<Category, Item>>(categoryList);
        }

        public IEnumerable<Grouping<Category, Item>> Categories { get; set; }
    }
}
