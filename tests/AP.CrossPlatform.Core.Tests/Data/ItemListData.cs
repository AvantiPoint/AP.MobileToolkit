using System;
using System.Collections.Generic;
using AP.CrossPlatform.Core.Tests.Mocks;

namespace AP.CrossPlatform.Core.Tests.Data
{
    public static class ItemListData
    {
        public static List<ObservableItemMock> ItemList = new List<ObservableItemMock>
        {
            new ObservableItemMock
            {
                Name = "Sail Boat",
                Group = "Marine",
                SubGroup = GroupMock.GroupA
            },
            new ObservableItemMock
            {
                Name = "Motor Boat",
                Group = "Marine",
                SubGroup = GroupMock.GroupA
            },
            new ObservableItemMock
            {
                Name = "Boat",
                Group = "Marine",
                SubGroup = GroupMock.GroupB
            },
            new ObservableItemMock
            {
                Name = "RC Car",
                Group = "Toy",
                SubGroup = GroupMock.GroupA
            },
            new ObservableItemMock
            {
                Name = "Bike",
                Group = "Foo",
                SubGroup = GroupMock.GroupC
            },
        };
    }
}
