using System.Collections.Generic;
using System.Collections.ObjectModel;
using AP.CrossPlatform.Collections;
using ToolkitDemo.Models;

namespace ToolkitDemo.SideMenu
{
    public interface IMenu
    {
        ObservableCollection<Grouping<Category, Item>> Categories { get; set; }
        IList<Item> MenuItems { get; set; }
    }
}
