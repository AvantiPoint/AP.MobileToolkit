using System;
using System.Linq;
using AP.CrossPlatform.Collections;
using Xunit;
using Xunit.Abstractions;
using AP.CrossPlatform.Core.Tests.Data;
using AP.CrossPlatform.Core.Tests.Mocks;

namespace AP.CrossPlatform.Core.Tests.Fixtures.Collections
{
    public class GroupingFixture : FixtureBase
    {
        public GroupingFixture(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Grouping_IsGrouped()
        {
            var data = ItemListData.ItemList;
            var sorted = from item in data
                         orderby item.Name
                         group item by item.Group into itemGroup
                         select new Grouping<string, ObservableItemMock>( itemGroup.Key, itemGroup );

            Assert.NotNull( sorted );
            Assert.True( data.Count() > sorted.Count() );
            Assert.True( sorted.Count() == 3 );
            var marine = sorted.FirstOrDefault( x => x.Key == "Marine" );
            var toy = sorted.FirstOrDefault( x => x.Key == "Toy" );
            var foo = sorted.FirstOrDefault( x => x.Key == "Foo" );

            Assert.NotNull( marine );
            Assert.NotNull( toy );
            Assert.NotNull( foo );

            Assert.True( marine.Count() == 3 );
            Assert.True( toy.Count() == 1 );
            Assert.True( foo.Count() == 1 );
        }

        [Fact]
        public void Grouping_IsGrouped_WithSubKey()
        {
            var data = ItemListData.ItemList;
            var sorted = from item in data
                         orderby item.Group, item.SubGroup, item.Name
                         group item by new { item.Group, item.SubGroup } into itemGroup
                         select new Grouping<string, GroupMock, ObservableItemMock>( itemGroup.Key.Group, itemGroup.Key.SubGroup, itemGroup );

            Assert.Equal( 4, sorted.Count() );
            var firstGroup = sorted.ElementAt( 0 );
            var secondGroup = sorted.ElementAt( 1 );
            var thirdGroup = sorted.ElementAt( 2 );
            var fourthGroup = sorted.ElementAt( 3 );

            Assert.Equal( "Foo", firstGroup.Key );
            Assert.Equal( GroupMock.GroupC, firstGroup.SubKey );
            Assert.Single(firstGroup);

            Assert.Equal( "Marine", secondGroup.Key );
            Assert.Equal( GroupMock.GroupA, secondGroup.SubKey );
            Assert.Equal( 2, secondGroup.Count() );

            Assert.Equal( "Marine", thirdGroup.Key );
            Assert.Equal( GroupMock.GroupB, thirdGroup.SubKey );
            Assert.Single(thirdGroup);

            Assert.Equal( "Toy", fourthGroup.Key );
            Assert.Equal( GroupMock.GroupA, fourthGroup.SubKey );
            Assert.Single(fourthGroup);
        }
    }
}
