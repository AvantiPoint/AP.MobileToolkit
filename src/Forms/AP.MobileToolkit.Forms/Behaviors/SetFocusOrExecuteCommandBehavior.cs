using System;
using System.Windows.Input;
using AP.CrossPlatform.Validations;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class SetFocusOrExecuteCommandBehavior : BehaviorBase<Entry>
    {
        public static readonly BindableProperty ModelProperty =
            BindableProperty.Create(nameof(Model), typeof(IValidatableModel), typeof(SetFocusOrExecuteCommandBehavior), null);

        public static readonly BindableProperty CheckModelValiditityProperty =
            BindableProperty.Create(nameof(CheckModelValiditity), typeof(bool), typeof(SetFocusOrExecuteCommandBehavior), false);

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(CommandProperty), typeof(ICommand), typeof(SetFocusOrExecuteCommandBehavior), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SetFocusOrExecuteCommandBehavior), null);

        public static readonly BindableProperty NextViewProperty =
            BindableProperty.Create(nameof(NextView), typeof(View), typeof(SetFocusOrExecuteCommandBehavior), null);

        public IValidatableModel Model
        {
            get => (IValidatableModel)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        public bool CheckModelValiditity
        {
            get => (bool)GetValue(CheckModelValiditityProperty);
            set => SetValue(CheckModelValiditityProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public View NextView
        {
            get => (View)GetValue(NextViewProperty);
            set => SetValue(NextViewProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Completed += OnCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Completed -= OnCompleted;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            if (Model == null)
            {
                return;
            }

            if (CheckModelValiditity)
            {
                Model.CheckModelState();
            }

            if (Model.IsValid && (Command?.CanExecute(CommandParameter) ?? false))
            {
                AssociatedObject.Unfocus();
                Command.Execute(CommandParameter);
            }
            else
            {
                NextView?.Focus();
            }
        }
    }
}
