using System;
using AP.CrossPlatform.Validations;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class ValidatableDatePickerBehavior : BehaviorBase<Picker>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(ValidatableDatePickerBehavior), null);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        protected override void OnAttachedTo(Picker bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        protected override void OnDetachingFrom(Picker bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.SelectedIndexChanged -= OnSelectedIndexChanged;
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Model?.CheckModelState();
        }
    }
}
