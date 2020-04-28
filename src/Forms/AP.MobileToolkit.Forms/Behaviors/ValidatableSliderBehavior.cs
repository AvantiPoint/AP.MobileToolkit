using AP.CrossPlatform.Validations;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class ValidatableSliderBehavior : BehaviorBase<Slider>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(ValidatableSliderBehavior), null);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        protected override void OnAttachedTo(Slider bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ValueChanged += OnValueChanged;
        }

        protected override void OnDetachingFrom(Slider bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.ValueChanged -= OnValueChanged;
        }

        private void OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            Model?.CheckModelState();
        }
    }
}
