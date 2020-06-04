using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public partial class DatePickerCell
    {
        public static readonly BindableProperty LabelProperty =
            BindableProperty.Create(nameof(Label), typeof(string), typeof(DatePickerCell), string.Empty);

        public static readonly BindableProperty SelectedDateProperty =
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(DatePickerCell), DateTime.Now.ToLocalTime(), propertyChanged: OnSelectedDateChanged);

        public static readonly new BindableProperty IsEnabledProperty =
            BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(DatePickerCell), true, propertyChanged: OnIsEnabledChanged);

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create(nameof(Format), typeof(string), typeof(DatePickerCell), "MMMM dd, yyyy");

        static void OnSelectedDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var date = (DateTime)newValue;
            var cell = bindable as DatePickerCell;

            if (cell.picker.Date == date)
            {
                return;
            }

            cell.picker.Date = date;
        }

        static void OnIsEnabledChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var cell = bindable as DatePickerCell;
            cell.picker.IsEnabled = (bool)newValue;
        }

        public DatePickerCell()
        {
            InitializeComponent();

            picker.MaximumDate = picker.Date = DateTime.Now.ToLocalTime();
            picker.MinimumDate = picker.MaximumDate.AddYears(-120);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public string Format
        {
            get => (string)GetValue(FormatProperty);
            set => SetValue(FormatProperty, value);
        }

        void Handle_DateSelected(object sender, DateChangedEventArgs e)
        {
            SelectedDate = e.NewDate;
        }
    }
}
