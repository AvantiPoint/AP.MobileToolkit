using System;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialDatePicker
    {
        public static BindableProperty CustomDateFormatProperty = BindableProperty.Create(nameof(CustomDateFormat), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);
        public string CustomDateFormat
        {
            get => (string)GetValue(CustomDateFormatProperty);
            set => SetValue(CustomDateFormatProperty, value);
        }
        private const string _defaultDateFormat = "dddd, MMMM d, yyyy";

        public static BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (b, o, n) => {
            var matDatePicker = (MaterialDatePicker)b;
            matDatePicker.Picker.Date = (DateTime)n;
        });

        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialDatePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matDatePicker = (MaterialDatePicker)bindable;
            matDatePicker.EntryField.Placeholder = (string)newval;
            matDatePicker.HiddenLabel.Text = (string)newval;
        });

        public static BindableProperty IsPasswordProperty = 
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialDatePicker), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.EntryField.IsPassword = (bool)newVal;
            });

        public static BindableProperty KeyboardProperty = 
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialDatePicker), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.EntryField.Keyboard = (Keyboard)newVal;
            });

        public static BindableProperty AccentColorProperty = 
            BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialDatePicker), defaultValue: Color.Accent);

        public static BindableProperty InvalidColorProperty = 
            BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.UpdateValidation();
            });

        public static BindableProperty DefaultColorProperty = 
            BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.UpdateValidation();
            });

        public static BindableProperty IsValidProperty = 
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialDatePicker)bindable;
                matEntry.UpdateValidation();
            });

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }
        public Color DefaultColor
        {
            get => (Color)GetValue(DefaultColorProperty);
            set => SetValue(DefaultColorProperty, value);
        }
        public Color InvalidColor
        {
            get => (Color)GetValue(InvalidColorProperty);
            set => SetValue(InvalidColorProperty, value);
        }

        public DateTime? Date
        {
            get => (DateTime?)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }
        public Color AccentColor
        {
            get => (Color)GetValue(AccentColorProperty);
            set => SetValue(AccentColorProperty, value);
        }
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public MaterialDatePicker()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            EntryField.Focused += (s, a) =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    EntryField.Unfocus();
                    Picker.Focus();
                });
            };
            Picker.Focused += async (s, a) =>
            {
                HiddenBottomBorder.BackgroundColor = AccentColor;
                HiddenLabel.TextColor = AccentColor;
                HiddenLabel.IsVisible = true;
                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + 4, 200, Easing.BounceIn)
                 );
                    EntryField.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
                }
            };
            Picker.Unfocused += async (s, a) =>
            {
                if (IsValid)
                    HiddenLabel.TextColor = DefaultColor;
                Picker_DateSelected(s, new DateChangedEventArgs(Picker.Date, Picker.Date));
                if (string.IsNullOrEmpty(EntryField.Text))
                {
                    // animate both at the same time
                    await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.BounceIn)
                 );
                    EntryField.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
                }
            };

            Picker.DateSelected += Picker_DateSelected;
        }

        private void Picker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomDateFormat))
                CustomDateFormat = _defaultDateFormat;
            EntryField.Text = e.NewDate.ToString(CustomDateFormat, CultureInfo.CurrentCulture);
            Date = e.NewDate;
        }


        /// <summary>
        /// Updates view based on validation state
        /// </summary>
        private void UpdateValidation()
        {
            if (IsValid)
            {
                BottomBorder.BackgroundColor = DefaultColor;
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if (IsFocused)
                {
                    HiddenLabel.TextColor = AccentColor;
                }
                else
                {
                    HiddenLabel.TextColor = DefaultColor;
                }
            }
            else
            {
                BottomBorder.BackgroundColor = InvalidColor;
                HiddenBottomBorder.BackgroundColor = InvalidColor;
                HiddenLabel.TextColor = InvalidColor;
            }
        }
    }
}