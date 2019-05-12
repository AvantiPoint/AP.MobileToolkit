using System;
using System.ComponentModel;
using AP.CrossPlatform.Validations;
using Prism.Behaviors;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class ValidatableSwitchBehavior : BehaviorBase<Switch>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(ValidatableDatePickerBehavior), null);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        protected override void OnAttachedTo(Switch bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Toggled += OnSwitchToggled;
        }

        protected override void OnDetachingFrom(Switch bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Toggled -= OnSwitchToggled;
        }

        private void OnSwitchToggled(object sender, ToggledEventArgs e)
        {
            Model?.CheckModelState();
        }
    }
}
