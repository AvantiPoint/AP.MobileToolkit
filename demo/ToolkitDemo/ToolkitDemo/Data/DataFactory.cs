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
            Category uiControls = new Category { CategoryId = 1, CategoryTitle = "UI Controls" };
            Category httpControls = new Category { CategoryId = 2, CategoryTitle = "Http Client" };
            DataItems = new ObservableCollection<Item>()
            {
                new Item
                {
                    ItemId = 1,
                    ItemTitle = "Homepage",
                    ItemUri = "NavigationPage/HomePage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 2,
                    ItemTitle = "Datepicker",
                    ItemUri = "NavigationPage/BorderlessDatePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 3,
                    ItemTitle = "Timepicker",
                    ItemUri = "NavigationPage/BorderlessTimePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 4,
                    ItemTitle = "Borderless Entry",
                    ItemUri = "NavigationPage/BorderlessEntryPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 5,
                    ItemTitle = "Material Datepicker",
                    ItemUri = "NavigationPage/MaterialDatePickerPage",
                    Category = uiControls
                },
                new Item
                {
                    ItemId = 6,
                    ItemTitle = "API Client",
                    Category = httpControls
                }
            };
        }
    }
}
