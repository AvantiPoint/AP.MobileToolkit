using System;
using AP.CrossPlatform.Extensions;
using Xunit;

namespace AP.CrossPlatform.Core.Tests.Fixtures.Extensions
{
    public class DateTimeExtensionsFixture
    {
        [Fact]
        public void SubtractMonths()
        {
            var starting = new DateTime( 2017, 3, 1 );
            var expected = new DateTime( 2017, 1, 1 );

            Assert.NotEqual(default(DateTime), starting);
            Assert.NotEqual(default(DateTime), expected);

            Assert.Equal( expected, starting.SubtractMonths( 2 ) );
            Assert.Equal( starting.AddMonths( -2 ), starting.SubtractMonths( 2 ) );
        }

        [Fact]
        public void SubtractDays()
        {
            var starting = new DateTime( 2017, 1, 3 );
            var expected = new DateTime( 2017, 1, 1 );

            Assert.NotEqual(default(DateTime), starting);
            Assert.NotEqual(default(DateTime), expected);

            Assert.Equal( expected, starting.SubtractDays( 2 ) );
            Assert.Equal( starting.AddDays( -2 ), starting.SubtractDays( 2 ) );
        }

        [Fact]
        public void SubtractHours()
        {
            var starting = new DateTime( 2017, 1, 1, 2, 0, 0 );
            var expected = new DateTime( 2017, 1, 1, 0, 0, 0 );

            Assert.NotEqual(default(DateTime), starting);
            Assert.NotEqual(default(DateTime), expected);

            Assert.Equal( expected, starting.SubtractHours( 2 ) );
            Assert.Equal( starting.AddHours( -2 ), starting.SubtractHours( 2 ) );
        }

        [Fact]
        public void SubtractMinutes()
        {
            var starting = new DateTime( 2017, 1, 1, 0, 2, 0 );
            var expected = new DateTime( 2017, 1, 1, 0, 0, 0 );

            Assert.NotEqual(default(DateTime), starting);
            Assert.NotEqual(default(DateTime), expected);

            Assert.Equal( expected, starting.SubtractMinutes( 2 ) );
            Assert.Equal( starting.AddMinutes( -2 ), starting.SubtractMinutes( 2 ) );
        }
    }
}
