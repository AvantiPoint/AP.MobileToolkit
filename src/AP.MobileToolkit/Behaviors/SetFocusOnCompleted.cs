using System;
using Prism.Behaviors;
using Xamarin.Forms;

namespace AP.MobileToolkit.Behaviors
{
    public class SetFocusOnCompleted : BehaviorBase<Entry>
    {
        public static readonly BindableProperty NextViewProperty =
            BindableProperty.Create(nameof(NextView), typeof(View), typeof(SetFocusOnCompleted), null);

        public View NextView
        {
            get => (View)GetValue(NextViewProperty);
            set => SetValue(NextViewProperty, value);
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject.Completed += OnCompleted;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject.Completed -= OnCompleted;
        }

        private void OnCompleted(object sender, EventArgs e)
        {
            NextView?.Focus();
        }
    }
}