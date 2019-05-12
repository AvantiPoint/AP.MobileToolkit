using System;
using AP.CrossPlatform.Validations;
using Prism.Behaviors;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class ValidatableSwitchCellBehavior : BehaviorBase<SwitchCell>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(ValidatableDatePickerBehavior), null);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        protected override void OnAttachedTo(SwitchCell bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.OnChanged += OnSwitchToggled;
        }

        protected override void OnDetachingFrom(SwitchCell bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.OnChanged -= OnSwitchToggled;
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            Model?.CheckModelState();
        }
    }
}
