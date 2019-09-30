﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Controls
{
    public partial class MaterialEntry : ContentView
    {
        #region Events

        public event EventHandler<FocusEventArgs> EntryFocused;

        public event EventHandler<FocusEventArgs> EntryUnfocused;

        public event EventHandler<TextChangedEventArgs> TextChanged;

        #endregion

        #region Bindable Properties

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(MaterialEntry), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.EntryField.Placeholder = (string)newval;
                matEntry.HiddenLabel.Text = (string)newval;
            });

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(MaterialEntry), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.EntryField.IsPassword = (bool)newVal;
            });

        public static readonly BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(MaterialEntry), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.EntryField.Keyboard = (Keyboard)newVal;
            });

        public static readonly BindableProperty AccentColorProperty =
            BindableProperty.Create(nameof(AccentColor), typeof(Color), typeof(MaterialEntry), defaultValue: Color.Accent);

        public static readonly BindableProperty InvalidColorProperty =
            BindableProperty.Create(nameof(InvalidColor), typeof(Color), typeof(MaterialEntry), Color.Red, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.UpdateValidation();
            });

        public static readonly BindableProperty DefaultColorProperty =
            BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(MaterialEntry), Color.Gray, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.UpdateValidation();
            });

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(MaterialEntry), true, propertyChanged: (bindable, oldVal, newVal) =>
            {
                var matEntry = (MaterialEntry)bindable;
                matEntry.UpdateValidation();
            });

        #endregion

        #region Public Properties

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

        #endregion

        public MaterialEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            BottomBorder.BackgroundColor = DefaultColor;
            EntryField.TextChanged += (s, a) =>
            {
                TextChanged?.Invoke(s, a);
            };

            EntryField.Focused += async (s, a) =>
            {
                EntryFocused?.Invoke(this, a);
                await CalculateLayoutFocused();
            };
            EntryField.Unfocused += async (s, a) =>
            {
                EntryUnfocused?.Invoke(this, a);
                await CalculateLayoutUnfocused();
            };
            EntryField.PropertyChanged += async (sender, args) =>
            {
                if (args.PropertyName == nameof(EntryField.Text) && !EntryField.IsFocused && !string.IsNullOrEmpty(EntryField.Text))
                {
                    await CalculateLayoutUnfocused();
                }
            };

            UpdateValidation();
        }

        /// <summary>
        /// Calculates the layout when unfocused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        /// <returns>The layout unfocused.</returns>
        private async Task CalculateLayoutUnfocused()
        {
            if (IsValid)
            {
                HiddenLabel.TextColor = DefaultColor;
                BottomBorder.BackgroundColor = DefaultColor;
            }
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(0, 180),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y, 200, Easing.BounceIn));

                EntryField.Placeholder = Placeholder;
            }
            else
            {
                HiddenLabel.IsVisible = true;
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height), 200);
            }
        }

        /// <summary>
        /// Calculates the layout when focused. Includes running the animation to update the bottom border color and the floating label
        /// </summary>
        private async Task CalculateLayoutFocused()
        {
            HiddenLabel.IsVisible = true;
            HiddenLabel.TextColor = AccentColor;
            HiddenBottomBorder.BackgroundColor = AccentColor;
            if (string.IsNullOrEmpty(EntryField.Text))
            {
                // animate both at the same time
                await Task.WhenAll(
                    HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200),
                    HiddenLabel.FadeTo(1, 60),
                    HiddenLabel.TranslateTo(HiddenLabel.TranslationX, EntryField.Y - EntryField.Height + 4, 200, Easing.BounceIn));

                EntryField.Placeholder = null;
            }
            else
            {
                await HiddenBottomBorder.LayoutTo(new Rectangle(BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height), 200);
            }
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
