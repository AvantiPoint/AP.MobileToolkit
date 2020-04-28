using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using AP.CrossPlatform;
using AP.CrossPlatform.Extensions;
using AP.MobileToolkit.Extensions;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace AP.MobileToolkit.Controls
{
    /// <summary>
    /// Class RadioGroup.
    /// </summary>
    public class RadioGroup : StackLayout
    {
        /// <summary>
        /// The items
        /// </summary>
        public ObservableCollection<CustomRadioButton> Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioGroup"/> class.
        /// </summary>
        public RadioGroup()
        {
            Items = new ObservableCollection<CustomRadioButton>();
        }

        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RadioGroup), default(IEnumerable), propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// The selected index property
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RadioGroup), -1, BindingMode.TwoWay, propertyChanged: OnSelectedIndexChanged);

        /// <summary>
        /// The selected item property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RadioGroup), null);

        /// <summary>
        /// The text color property
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(RadioGroup), Color.Black);

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RadioGroup), -1.0);

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create(nameof(FontName), typeof(string), typeof(RadioGroup), string.Empty);

        private static void OnSelectedIndexChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if ((int)newvalue == -1)
            {
                return;
            }

            if (!(bindable is RadioGroup bindableRadioGroup))
            {
                return;
            }

            foreach (var button in bindableRadioGroup.Items.Where(button => button.Id == bindableRadioGroup.SelectedIndex))
            {
                button.Checked = true;
            }

            bindableRadioGroup.SelectedItem = bindableRadioGroup.ItemsSource
                                                                .Cast<object>()
                                                                .ElementAt(bindableRadioGroup.SelectedIndex);
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var radButtons = bindable as RadioGroup;

            foreach (var item in radButtons.Items)
            {
                item.CheckedChanged -= radButtons.OnCheckedChanged;
            }

            radButtons.Children.Clear();

            var radIndex = 0;

            foreach (var item in radButtons.ItemsSource)
            {
                var button = new CustomRadioButton
                {
                    Text = item.ToString(),
                    Id = radIndex++,
                    TextColor = radButtons.TextColor,
                    FontSize = Xamarin.Forms.Device.GetNamedSize(NamedSize.Small, radButtons),
                    FontName = radButtons.FontName
                };

                button.CheckedChanged += radButtons.OnCheckedChanged;

                radButtons.Items.Add(button);

                radButtons.Children.Add(button);
            }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get { return this.GetValue<IEnumerable>(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the index of the selected.
        /// </summary>
        /// <value>The index of the selected.</value>
        public int SelectedIndex
        {
            get { return this.GetValue<int>(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return this.GetValue<Color>(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get { return this.GetValue<double>(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get { return this.GetValue<string>(FontNameProperty); }
            set { SetValue(FontNameProperty, value); }
        }

        /// <summary>
        /// Occurs when [checked changed].
        /// </summary>
        public event EventHandler<int> CheckedChanged;

        private void OnCheckedChanged(object sender, EventArgs<bool> e)
        {
            if (e.Value == false)
            {
                return;
            }

            if (!(sender is CustomRadioButton selectedItem))
            {
                return;
            }

            foreach (var item in Items)
            {
                if (!selectedItem.Id.Equals(item.Id))
                {
                    item.Checked = false;
                }
                else
                {
                    SelectedIndex = selectedItem.Id;
                    if (CheckedChanged != null)
                    {
                        CheckedChanged.Invoke(sender, item.Id);
                    }
                }
            }
        }
    }
}
