using AP.CrossPlatform;
using AP.MobileToolkit.Controls;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class SelectableMock : ObservableObject, ISelectable
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}
