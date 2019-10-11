using AP.CrossPlatform;

namespace ToolkitDemo.Models
{
    public class Category : ISelectable
    {
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
