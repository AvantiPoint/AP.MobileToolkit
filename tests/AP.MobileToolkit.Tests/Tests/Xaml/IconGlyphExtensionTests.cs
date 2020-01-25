using System;
using System.Collections.Generic;
using System.Text;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Tests.Mocks;
using AP.MobileToolkit.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xunit;

namespace AP.MobileToolkit.Tests.Tests.Xaml
{
    public class IconGlyphExtensionTests
    {
        private const string TestFontFileName = "foobar.ttf";

        [Theory]
        [InlineData("test-phonegap", TestFont.Phonegap)]
        [InlineData("test-googledrive", TestFont.GoogleDrive)]
        [InlineData("test-google-drive", TestFont.GoogleDrive)]
        [InlineData("test-google_drive", TestFont.GoogleDrive)]
        [InlineData("test-html5", TestFont.Html5)]
        [InlineData("test-3drotation", TestFont._3dRotation)]
        [InlineData("test-3d-rotation", TestFont._3dRotation)]
        public void SetsTextAndFontFamily(string iconName, string expectedGlyph)
        {
            IconFontRegistry.Reset();
            IconFontRegistry.Register(new IconFont(TestFontFileName, "test", typeof(TestFont)));

            var label = new Label();
            var services = new ServiceCollection();
            services.AddSingleton<IProvideValueTarget>(new ProvideValueTargetMock { TargetObject = label, TargetProperty = Label.TextProperty });
            var sp = services.BuildServiceProvider();

            var extension = new IconGlyphExtension { IconName = iconName };
            var ex = Record.Exception(() => label.Text = extension.ProvideValue(sp));

            Assert.Null(ex);
            Assert.Equal(expectedGlyph, label.Text);
            Assert.Equal(TestFontFileName, label.FontFamily);
        }

        private static class TestFont
        {
            public const string Phonegap = "\ue630";
            public const string GoogleDrive = "\ue631";
#pragma warning disable SA1310 // Field names should not contain underscore
            public const string Html5_Multimedia = "\ue632";
            public const string Html5_DeviceAccess = "\ue633";
            public const string Html5_Connectivity = "\ue634";
            public const string Html5_3dEffects = "\ue635";
#pragma warning restore SA1310 // Field names should not contain underscore
            public const string Html5 = "\ue636";
            public const string _3dRotation = "\ue84d";
        }
    }
}
