using System;
using System.Collections.Generic;
using System.Text;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Tests.Mocks.Fonts;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Fonts
{
    public class IconSpanTests : TestBase
    {
        public IconSpanTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void TextIsSetFromIconName()
        {
            IconFontRegistry.Reset();
            IconFontRegistry.Register(new IconFont("TestFontFamily", "test", typeof(MockFontA)));

            var span = new IconSpan { GlyphName = "test-foo" };

            Assert.Single(span.Text);
            Assert.Equal(MockFontA.Foo, span.Text);
        }
    }
}
