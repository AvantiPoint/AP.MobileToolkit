using AP.CrossPlatform;

namespace ToolkitDemo.Models
{
    public class SelectableItem : ObservableObject, ISelectable
    {
        public bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        public string Text { get; set; }
    }
}
