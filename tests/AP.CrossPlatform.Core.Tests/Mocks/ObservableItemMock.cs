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
            get { return _name; }
            set { SetProperty( ref _name, value ); }
        }

        public string Group
        {
            get { return _group; }
            set { SetProperty( ref _group, value, onChanged: () => GroupChanged++ ); }
        }

        public GroupMock SubGroup
        {
            get { return _subGroup; }
            set { SetProperty( ref _subGroup, value ); } 
        }

        public int GroupChanged { get; private set; }
    }
}
