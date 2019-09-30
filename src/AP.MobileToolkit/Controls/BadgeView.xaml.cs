using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Controls
{
    public partial class BadgeView
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(BadgeView), "0", propertyChanged: (bindable, oldVal, newVal) =>
            {
                var view = (BadgeView)bindable;
                view.BadgeLabel.Text = (string)newVal;
            });

        public static readonly BindableProperty BadgeColorProperty =
            BindableProperty.Create(nameof(BadgeColor), typeof(Color), typeof(BadgeView), Color.Blue, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var view = (BadgeView)bindable;
                view.BadgeCircle.BackgroundColor = (Color)newVal;
            });

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color BadgeColor
        {
            get => (Color)GetValue(BadgeColorProperty);
            set => SetValue(BadgeColorProperty, value);
        }

        public BadgeView()
        {
            InitializeComponent();
            BadgeLabel.Text = Text;
            BadgeCircle.BackgroundColor = BadgeColor;
        }
    }
}
