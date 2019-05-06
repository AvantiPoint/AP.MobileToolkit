using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AP.MobileToolkit.Tests.Tests;
using Xunit;
using Xunit.Abstractions;

namespace AP.CrossPlatform.Auth.Tests
{
    public class IUserTests : TestBase
    {
        public IUserTests(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void MissingKeyReturnsEmptyArray()
        {
            var user = new JwtUser();
            var value = user.GetStringArrayValue("DoesNotExist");
            Assert.NotNull(value);
            Assert.IsType<string[]>(value);
        }

        [Fact]
        public void InvalidValueReturnsArrayWithTrimedValue()
        {
            var user = new JwtUser
            {
                { "Test", "Not A Real Thing" }
            };
            IEnumerable<string> value = null;
            var ex = Record.Exception(() => value = user.GetStringArrayValue("Test"));
            Assert.Null(ex);
            Assert.Single(value);
            Assert.Equal("Not A Real Thing", value.First());
        }

        [Fact]
        public void ReturnsDefaultDateTimeWhenValueDoesNotExist()
        {
            var user = new JwtUser();
            var now = DateTime.Now;
            var doesNotExist = user.GetDateTimeValue("DoesNotExist", now);
            Assert.Equal(now, doesNotExist);
        }

        [Fact]
        public  void ReturnsDateFromDateString()
        {
            var now = new DateTime(2018, 8, 25, 12, 0, 0);
            var user = new JwtUser()
            {
                { "Test", now.ToString() }
            };
            var converted = user.GetDateTimeValue("Test");
            Assert.Equal(now, converted);
        }

        [Fact]
        public void ReturnsDefaultDateTimeWithInvalidInput()
        {
            var user = new JwtUser
            {
                { "Test", "This is Not A Valid DateTime" }
            };
            var now = DateTime.Now;
            var converted = user.GetDateTimeValue("Test", now);
            Assert.Equal(now, converted);
        }

        [Fact]
        public void SetNullDateTimeSetsNullString()
        {
            var user = new JwtUser();
            user.SetDateTimeValue("Test", null);
            Assert.True(user.ContainsKey("Test"));
            Assert.Null(user["Test"]);
        }

        [Fact]
        public void SetDateTimeStoresDateTime()
        {
            var user = new JwtUser();
            var date = new DateTime(2018, 8, 25, 12, 0, 0, DateTimeKind.Utc);
            user.SetDateTimeValue("Test", date);
            Assert.Equal("1535198400", user["Test"]);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(true)]
        [InlineData(false)]
        public void MissingKeyReturnsDefaultNullableBool(bool? expected)
        {
            var user = new JwtUser();
            var value = user.GetBoolValue("Test", expected);
            Assert.Equal(expected, value);
        }

        [Theory]
        [InlineData("0", false)]
        [InlineData("1", true)]
        [InlineData("false", false)]
        [InlineData("False", false)]
        [InlineData("FALSE", false)]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("TRUE", true)]
        public void StringConvertsToBool(string input, bool? expected)
        {
            var user = new JwtUser
            {
                { "Test", input }
            };
            var value = user.GetBoolValue("Test");
            Assert.Equal(expected, value);
        }

        [Fact]
        public void InvalidInputReturnsDefaultBool()
        {
            var user = new JwtUser
            {
                { "Test", "Not A Real Thing" }
            };
            var value = user.GetBoolValue("Test", true);
            Assert.True(value);
        }

        [Fact]
        public void MissingKeyReturnsDefaultLong()
        {
            var user = new JwtUser();
            var value = user.GetLongValue("Not A Thing");
            Assert.Null(value);
            value = user.GetLongValue("Not A Thing", 5);
            Assert.Equal(5, value);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("100", 100)]
        public void GetsLong(string input, long? expected)
        {
            var user = new JwtUser
            {
                { "Test", input }
            };
            var value = user.GetLongValue("Test");
            Assert.Equal(expected, value);
        }

        [Fact]
        public void InvalidValueReturnsDefaultLong()
        {
            var user = new JwtUser
            {
                { "Test", "Not A Real Thing" }
            };
            var value = user.GetLongValue("Test", 32);
            Assert.Equal(32, value);
        }
    }
}
