using System.Collections.Generic;
using System.Windows.Input;
using AP.MobileToolkit.Menus;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuBar
    {
        public static readonly BindableProperty MenuOptionsProperty =
            BindableProperty.Create(nameof(MenuOptions), typeof(IEnumerable<MainMenuOption>), typeof(MenuBar), null);

        public static readonly BindableProperty NavigationCommandProperty =
            BindableProperty.Create(nameof(NavigationCommand), typeof(ICommand), typeof(MenuBar), null);

        public MenuBar()
        {
            InitializeComponent();
        }

        public IEnumerable<MainMenuOption> MenuOptions
        {
            get => (IEnumerable<MainMenuOption>)GetValue(MenuOptionsProperty);
            set => SetValue(MenuOptionsProperty, value);
        }

        public ICommand NavigationCommand
        {
            get => (ICommand)GetValue(NavigationCommandProperty);
            set => SetValue(NavigationCommandProperty, value);
        }
    }
}
