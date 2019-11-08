using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Tests.Mocks.Fonts;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Fonts
{
    public class FontRegistryTests : TestBase
    {
        public FontRegistryTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void FontRegistryDoesNotThrowException()
        {
            var ex = Record.Exception(() => RegisterTestIcons());
            Assert.Null(ex);
        }

        [Fact]
        public void TestFontIsRegistered()
        {
            RegisterTestIcons();

            Assert.Single(IconFontRegistry.Instance.RegisteredFonts);

            var iconFont = IconFontRegistry.Instance.RegisteredFonts[0];

            Assert.Equal("TestFontFamily", iconFont.FontName);
            Assert.Equal("test", iconFont.Prefix);
            Assert.Equal(2, iconFont.Icons.Count());
        }

        [Theory]
        [InlineData("test-foo", MockFontA.Foo)]
        [InlineData("test-foo-bar", MockFontA.FooBar)]
        public void RegistryReturnsExpectedChar(string icon, string glyph)
        {
            RegisterTestIcons();

            Assert.True(IconFontRegistry.Instance.TryFindIconForKey(icon, out var iconInfo));

            Assert.Equal(glyph, iconInfo.Glyph);
        }

        private void RegisterTestIcons()
        {
            IconFontRegistry.Reset();
            IconFontRegistry.Register(new IconFont("TestFontFamily", "test", typeof(MockFontA)));
        }
    }
}
