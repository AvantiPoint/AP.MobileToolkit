﻿using System;
using AP.MobileToolkit.Fonts;
using Prism;
using Prism.Ioc;
using Prism.Logging;
using ToolkitDemo.Services;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ToolkitDemo
{
    [AutoRegisterForNavigation]
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

            var result = await NavigationService.NavigateAsync("MainPage/NavigationPage/HomePage");

            if (!result.Success)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.Register<ILogger, ConsoleLoggingService>();
            containerRegistry.RegisterSingleton<IMenuService, MenuService>();
            containerRegistry.Register<IClipboard, ClipboardImplementation>();
            containerRegistry.Register<ICodeSampleResolver, CodeSampleResolver>();
        }
    }
}
