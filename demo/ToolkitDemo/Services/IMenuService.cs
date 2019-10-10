using System.Collections.Generic;
using AP.CrossPlatform.Collections;
using ToolkitDemo.Models;

namespace ToolkitDemo.Services
{
    public interface IMenuService
    {
        IEnumerable<Grouping<Category, Item>> Categories { get; set; }
    }
}
