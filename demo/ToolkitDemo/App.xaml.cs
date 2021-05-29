using System;
using AP.CrossPlatform.i18n;
using AP.MobileToolkit.Fonts;
using AP.MobileToolkit.Mvvm;
using Prism;
using Prism.Ioc;
using Prism.Logging;
using ToolkitDemo.Helpers;
using ToolkitDemo.Services;
using ToolkitDemo.ViewModels;
using ToolkitDemo.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToolkitDemo
{
    public partial class App
    {
        public App()
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            FontRegistry.RegisterFonts(FontAwesomeBrands.Font, FontAwesomeRegular.Font, FontAwesomeSolid.Font);

            var result = await NavigationService.NavigateAsync("MainPage/NavigationPage/HomePage");

            if (!result.Success)
            {
                MainPage = result.Exception.ToErrorPage();
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.UseAggregateLogger();
            containerRegistry.RegisterSingleton<ILocalize, ResxLocalize>();
            containerRegistry.RegisterSingleton<IMenuService, MenuService>();
            containerRegistry.Register<IClipboard, ClipboardImplementation>();
            containerRegistry.Register<ICodeSampleResolver, CodeSampleResolver>();

            containerRegistry.RegisterForNavigation<APIClientPage, APIClientPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<ImageEntryPage, ImageEntryPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<ShowCodePage, ShowCodePageViewModel>();
        }
    }
}
