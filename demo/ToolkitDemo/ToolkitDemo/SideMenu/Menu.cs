using System.Collections.Generic;
using System.Collections.ObjectModel;
using AP.CrossPlatform.Collections;
using ToolkitDemo.Models;

namespace ToolkitDemo.SideMenu
{
    public class Menu : IMenu
    {
        public ObservableCollection<Grouping<Category, Item>> Categories { get; set; }
        public IList<Item> MenuItems { get; set; }
        private IList<Category> itemCategories;

        public Menu()
        {
            itemCategories = new ObservableCollection<Category>()
            {
                new Category { CategoryId = 1, CategoryTitle = "Home", IsSelected = true },
                new Category { CategoryId = 2, CategoryTitle = "UI Controls", IsSelected = false },
                new Category { CategoryId = 3, CategoryTitle = "Http Client", IsSelected = false },
                new Category { CategoryId = 4, CategoryTitle = "Converters", IsSelected = false }
            };

            MenuItems = new ObservableCollection<Item>()
            {
                new Item
                {
                    ItemId = 1,
                    ItemTitle = "Borderless Datepicker",
                    ItemUri = "NavigationPage/BorderlessDatePickerPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 2,
                    ItemTitle = "Borderless Timepicker",
                    ItemUri = "NavigationPage/BorderlessTimePickerPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 3,
                    ItemTitle = "Borderless Entry",
                    ItemUri = "NavigationPage/BorderlessEntryPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 4,
                    ItemTitle = "Material Datepicker",
                    ItemUri = "NavigationPage/MaterialDatePickerPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 5,
                    ItemTitle = "Material Timepicker",
                    ItemUri = "NavigationPage/MaterialTimePickerPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 6,
                    ItemTitle = "Material Entry",
                    ItemUri = "NavigationPage/MaterialEntryPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 7,
                    ItemTitle = "Badge View",
                    ItemUri = "NavigationPage/BadgeViewPage",
                    CategoryId = 2
                },
                new Item
                {
                    ItemId = 8,
                    ItemTitle = "API Client",
                    ItemUri = "NavigationPage/HomePage",
                    CategoryId = 3
                },

                // new Item
                // {
                //    ItemId = 9,
                //    ItemTitle = "Image Entry",
                //    ItemUri = "NavigationPage/ImageEntryPage",
                //    CategoryId = 2
                // },
                // new Item
                // {
                //    ItemId = 10,
                //    ItemTitle = "Radio Button",
                //    ItemUri = "NavigationPage/RadioButtonPage",
                //    CategoryId = 2
                // },
                new Item
                {
                    ItemId = 11,
                    ItemTitle = "Uppercase Converter",
                    ItemUri = "NavigationPage/UppercaseConverterPage",
                    CategoryId = 4
                },
            };

            Categories = new ObservableCollection<Grouping<Category, Item>>();

            foreach (var itemCatergory in itemCategories)
            {
                Categories.Add(new Grouping<Category, Item>(itemCatergory, new List<Item>()));
            }
        }
    }
}
