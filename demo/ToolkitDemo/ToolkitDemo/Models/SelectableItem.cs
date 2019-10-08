using System.ComponentModel;
using System.Runtime.CompilerServices;
using AP.CrossPlatform;

namespace ToolkitDemo.Models
{
    public class SelectableItem : ISelectable, INotifyPropertyChanged
    {
        public bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                NotifyPropertyChanged();
            }
        }
        public string Text { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
