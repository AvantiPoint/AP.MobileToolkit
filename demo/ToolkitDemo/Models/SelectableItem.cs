using AP.CrossPlatform;

namespace ToolkitDemo.Models
{
    public class SelectableItem : ObservableObject, ISelectable
    {
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
        private bool _isSelected;

        public string Text { get; set; }
    }
}
