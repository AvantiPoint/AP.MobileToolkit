using System;
using System.Collections.Generic;
using System.Linq;
using AP.MobileToolkit.Tests.Tests;
using Xunit;
using Xunit.Abstractions;

namespace AP.CrossPlatform.Auth.Tests
{
    public class JwtUserTests : TestBase
    {
        public const string Jwt = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJleHAiOjE1MjY4NDQzNjIsIm5iZiI6MTUyNjg0MDc2MiwidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnRvbmxpbmUuY29tLzZlY2VlZDllLTMyYTktNDk5My1hYjFmLWJhODM5ODlkZjc0YS92Mi4wLyIsInN1YiI6Ijc5NTFmNTU4LWRlMjItNDViZi05ZjQwLWIwYTM3M2EyNGU5ZiIsImF1ZCI6IjlmZGM1ZmYwLTdlZGItNDcwMy05MThiLTY4ZTAwYmQ3ZWFkMyIsImlhdCI6MTUyNjg0MDc2MiwiYXV0aF90aW1lIjoxNTI2ODQwNzYyLCJvaWQiOiI3OTUxZjU1OC1kZTIyLTQ1YmYtOWY0MC1iMGEzNzNhMjRlOWYiLCJlbWFpbHMiOlsiZHNpZWdlbEBhdmFudGlwb2ludC5jb20iXSwibmV3VXNlciI6dHJ1ZSwiZmFtaWx5X25hbWUiOiJTaWVnZWwiLCJnaXZlbl9uYW1lIjoiRGFuIiwidGZwIjoiQjJDXzFfc3VzaSJ9.jGz_VtBpQELIfXTf8lab_miX-ksaNDJmn1qYtOF2Lr4T3ff29M-3WvTG7MS72wsOf5Dm4L20g7MqK-P9IrlwRhjX_WdGfxDgjcJhTiPtgsLv4DGD-gNjhgyYJhIDSD8VVfKHmXNoK2fXi7KIYVv-EkyvnjfmFUYFASPOxSZaIl26YNzYjAVUP5PwDRewPYcjPzxjYZbU5hvqfMWXznevuOGyhw8-bHMGfo__pZrBISpD6pVhBc930IvSG50xx8iy1LUsaMyrep6llGX23EHDsU6PTId18gt8o1lqVs6GxSGLXhlnp2UcaXM2bh7Fwi57F7wCYAoCf0oFhFwejp1kZQ";

        public JwtUserTests(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void DoesNotThrowException()
        {
            var exception = Record.Exception(() => new JwtUser(Jwt));
            Assert.Null(exception);
        }

        [Fact]
        public void NullableDateTimeConversionReturnsCorrectValue()
        {
            IUser user = new JwtUser(Jwt);
            var exp = user.Expiration;

            Assert.True(exp.HasValue);
            Assert.Equal(DateTime.Parse("Sunday, May 20, 2018 7:26:02 PM"), exp.Value);
        }

        [Fact]
        public void StringArrayHasASingleValue()
        {
            IUser user = new JwtUser(Jwt);
            var emails = user.GetStringArrayValue("emails");

            _testOutputHelper.WriteLine(user["emails"]);
            Assert.NotNull(emails);
            Assert.Single(emails);
            Assert.Equal("dsiegel@avantipoint.com", user.Email);
        }

        [Fact]
        public void StringArraySerializesMultipleValues()
        {
            IUser user = new JwtUser(Jwt);
            var key = "test";
            user.Add(key, "[\"valueA\",\"valueB\"]");

            Assert.True(user.ContainsKey(key));
            IEnumerable<string> values = null;
            var exception = Record.Exception(() => values = user.GetStringArrayValue(key));

            Assert.Null(exception);
            Assert.NotNull(values);
            Assert.Equal(2, values.Count());
            Assert.Equal("valueA", values.ElementAt(0));
            Assert.Equal("valueB", values.ElementAt(1));
        }
    }
}