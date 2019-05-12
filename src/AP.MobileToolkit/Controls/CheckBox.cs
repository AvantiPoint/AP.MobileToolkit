using System;
using AP.CrossPlatform;
using AP.CrossPlatform.Extensions;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    /// <summary>
    /// The check box.
    /// </summary>
    public class CheckBox : View
    {
        /// <summary>
        /// The checked state property.
        /// </summary>
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(nameof(Checked), typeof(bool), typeof(CheckBox), false, BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

        /// <summary>
        /// The checked text property.
        /// </summary>
        public static readonly BindableProperty CheckedTextProperty =
            BindableProperty.Create(nameof(CheckedText), typeof(string), typeof(CheckBox), string.Empty, BindingMode.TwoWay);

        /// <summary>
        /// The unchecked text property.
        /// </summary>
        public static readonly BindableProperty UncheckedTextProperty =
            BindableProperty.Create(nameof(UncheckedText), typeof(string), typeof(CheckBox), string.Empty);

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty DefaultTextProperty =
            BindableProperty.Create(nameof(DefaultText), typeof(string), typeof(CheckBox), string.Empty);

        /// <summary>
        /// Identifies the TextColor bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CheckBox), Color.Default);

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(CheckBox), -1);

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName), typeof(string), typeof(CheckBox), string.Empty);

        /// <summary>
        /// The checked changed event.
        /// </summary>
        public event EventHandler<EventArgs<bool>> CheckedChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get => (bool)GetValue(CheckedProperty);
            set
            {
                if (Checked != value)
                {
                    SetValue(CheckedProperty, value);
                    CheckedChanged.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the checked text.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string CheckedText
        {
            get => (string)GetValue(CheckedTextProperty);
            set => SetValue(CheckedTextProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        /// <remarks>
        /// Overwrites the default text property if set when checkbox is checked.
        /// </remarks>
        public string UncheckedText
        {
            get => (string)GetValue(UncheckedTextProperty);
            set => SetValue(UncheckedTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string DefaultText
        {
            get => (string)GetValue(DefaultTextProperty);
            set => SetValue(DefaultTextProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get => (string)GetValue(FontNameProperty);
            set => SetValue(FontNameProperty, value);
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                return Checked
                    ? (string.IsNullOrEmpty(CheckedText) ? DefaultText : CheckedText)
                        : (string.IsNullOrEmpty(UncheckedText) ? DefaultText : UncheckedText);
            }
        }

        /// <summary>
        /// Called when [checked property changed].
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <param name="oldvalue">if set to <c>true</c> [oldvalue].</param>
        /// <param name="newvalue">if set to <c>true</c> [newvalue].</param>
        private static void OnCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var checkBox = (CheckBox)bindable;
            checkBox.Checked = (bool)newvalue;
        }
    }
}
