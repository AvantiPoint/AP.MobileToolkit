using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToolkitDemo.Models;

namespace ToolkitDemo.Data
{
    public class DataFactory
    {
        public static IList<Item> DataItems { get; private set; }

        static DataFactory()
        {
            Category homePage = new Category { CategoryId = 1, CategoryTitle = "Home Page" };
            Category uiControls = new Category { CategoryId = 1, CategoryTitle = "UI Controls" };
            Category httpControls = new Category { CategoryId = 2, CategoryTitle = "Http Client" };
            DataItems = new ObservableCollection<Item>()
            {
                new Item
                {
                    ItemId = 1,
                    ItemTitle = "Datepicker",
                    ItemUri = "NavigationPage/BorderlessDatePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 2,
                    ItemTitle = "Timepicker",
                    ItemUri = "NavigationPage/BorderlessTimePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 3,
                    ItemTitle = "Borderless Entry",
                    ItemUri = "NavigationPage/BorderlessEntryPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 4,
                    ItemTitle = "Material Datepicker",
                    ItemUri = "NavigationPage/MaterialDatePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 5,
                    ItemTitle = "Material Entry",
                    ItemUri = "NavigationPage/MaterialEntryPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 6,
                    ItemTitle = "Material Timepicker",
                    ItemUri = "NavigationPage/MaterialTimePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 7,
                    ItemTitle = "Badge View",
                    ItemUri = "NavigationPage/BadgeViewPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 8,
                    ItemTitle = "API Client",
                    ItemUri = "NavigationPage/HomePage",
                    Category = httpControls
                }
            };
        }
    }
}
