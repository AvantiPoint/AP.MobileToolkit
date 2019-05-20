using System.Collections.Generic;
using System.Linq;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Tests.Mocks.Fonts;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Fonts
{
    public class IconFontTests : TestBase
    {
        public IconFontTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void IconClassReturnsRegisteredIcons()
        {
            IEnumerable<RegisteredIcon> icons = null;

            var ex = Record.Exception(() => icons = IconFont.GetIcons(typeof(MockFontA)));

            Assert.Null(ex);
            Assert.NotNull(icons);
            Assert.Equal(2, icons.Count());
        }

        [Theory]
        [InlineData("test-foo", MockFontA.Foo)]
        [InlineData("test-Foo", MockFontA.Foo)]
        [InlineData("test-FooBar", MockFontA.FooBar)]
        [InlineData("test-foobar", MockFontA.FooBar)]
        [InlineData("test-Foo-Bar", MockFontA.FooBar)]
        [InlineData("test-foo-bar", MockFontA.FooBar)]
        public void LocatesIconWithKey(string key, string expectedGlyph)
        {
            var iconFont = new IconFont("Test.otf", "test", typeof(MockFontA));

            var glyph = iconFont.FindIconForKey(key);

            Assert.False(string.IsNullOrWhiteSpace(glyph));

            Assert.Equal(expectedGlyph, glyph);
        }
    }
}
