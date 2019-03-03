using System;
using AP.CrossPlatform.Validations;
using Prism.Behaviors;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class ValidatableEntryBehavior : BehaviorBase<Entry>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(ValidatableEntryBehavior), null);

        public static readonly BindableProperty ValidateOnChangedProperty =
            BindableProperty.Create(nameof(ValidateOnChanged), typeof(bool), typeof(ValidatableEntryBehavior), false);

        public static readonly BindableProperty ValidateOnCompletedProperty =
            BindableProperty.Create(nameof(ValidateOnCompleted), typeof(bool), typeof(ValidatableEntryBehavior), true);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public bool ValidateOnChanged
        {
            get => (bool)GetValue(ValidateOnChangedProperty);
            set => SetValue(ValidateOnChangedProperty, value);
        }

        public bool ValidateOnCompleted
        {
            get => (bool)GetValue(ValidateOnCompletedProperty);
            set => SetValue(ValidateOnCompletedProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnCompleted;
            bindable.TextChanged += OnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed -= OnCompleted;
            bindable.TextChanged -= OnTextChanged;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            if (ValidateOnCompleted)
            {
                Model?.CheckModelState();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (ValidateOnChanged)
            {
                Model?.CheckModelState();
            }
        }
    }
}