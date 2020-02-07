using AP.MobileToolkit.Fonts;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Fonts
{
    public class FontTests
    {
        private ITestOutputHelper testOutput;

        public FontTests(ITestOutputHelper testOutputHelper)
        {
            testOutput = testOutputHelper;
            Xamarin.Forms.Mocks.MockForms.Init();
        }

        [Theory]
        [InlineData("fa-500px", "\uf26e")]
        [InlineData("fa-angry", "\uf556")]
        [InlineData("fa-accessible-icon", "\uf368")]
        public void LookupByClassSelector(string selector, string glyph)
        {
            var font = new EmbeddedWebFont("foobar", "fa", "fontawesome.min.css", GetType());
            var resolvedGlyph = font.GetGlyph(selector);

            Assert.NotNull(resolvedGlyph);
            Assert.Single(resolvedGlyph);
            Assert.Equal(glyph, resolvedGlyph);
        }

        [Theory]
        [InlineData("fab fa-500px", "\uf26e")]
        [InlineData("far fa-angry", "\uf556")]
        [InlineData("fab fa-accessible-icon", "\uf368")]
        public void LookupByFullyQualifiedSelector(string selector, string glyph)
        {
            var font = new EmbeddedWebFont("foobar", "fab", "fontawesome.min.css", GetType());
            var resolvedGlyph = font.GetGlyph(selector);

            Assert.NotNull(resolvedGlyph);
            Assert.Single(resolvedGlyph);
            Assert.Equal(glyph, resolvedGlyph);
        }

        object lockobject = new object();
        [Theory]
        [InlineData("far fa-calendar", "\uf133", FontAwesomeRegular.FontName)]
        [InlineData("fas fa-calendar", "\uf133", FontAwesomeSolid.FontName)]
        public void LookupFromRegistry(string selector, string expectedGlyph, string fontFileName)
        {
            lock (lockobject)
            {
                FontRegistry.Clear();
                FontRegistry.RegisterFonts(FontAwesomeRegular.Font, FontAwesomeSolid.Font);

                var font = FontRegistry.LocateFont(selector);

                Assert.NotNull(font);

                Assert.Equal(fontFileName, font.FontFileName);

                var locatedGlyph = font.GetGlyph(selector);

                Assert.False(string.IsNullOrWhiteSpace(locatedGlyph));
                Assert.Equal(expectedGlyph, locatedGlyph);
            }
        }
    }
}
