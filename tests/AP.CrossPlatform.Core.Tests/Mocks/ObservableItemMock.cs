using System;

namespace AP.CrossPlatform.Core.Tests.Mocks
{
    public class ObservableItemMock : ObservableObject
    {
        private string _name;
        private string _group;
        private GroupMock _subGroup;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Group
        {
            get => _group;
            set => SetProperty(ref _group, value, onChanged: () => GroupChanged++);
        }

        public GroupMock SubGroup
        {
            get => _subGroup;
            set => SetProperty(ref _subGroup, value);
        }

        public int GroupChanged { get; private set; }
    }
}
