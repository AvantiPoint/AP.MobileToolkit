using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public class RepeaterView : StackLayout
    {
        public static readonly BindableProperty HeaderProperty =
            BindableProperty.Create(nameof(Header), typeof(View), typeof(RepeaterView), null, propertyChanged: ResetView);

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView), new DefaultDataTemplate(), propertyChanged: ResetView);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RepeaterView), null, defaultBindingMode: BindingMode.OneWay, propertyChanged: ItemsChanged);

        public RepeaterView()
        {
            Spacing = 0;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public View Header
        {
            get { return (View)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        protected virtual View ViewFor(object item)
        {
            View view = null;
            object content = null;

            switch (ItemTemplate)
            {
                case DataTemplateSelector selector:
                    var template = selector.SelectTemplate(item, this);
                    content = template.CreateContent();
                    break;
                case DataTemplate dataTemplate:
                    content = dataTemplate.CreateContent();
                    break;
            }

            view = (content is View) ? content as View : ((ViewCell)content)?.View;

            return view;
        }

        protected void UpdateChildren()
        {
            Children.Clear();

            if (ItemsSource?.Cast<object>()?.Any() ?? false)
            {
                if (Header != null)
                    Children.Add(Header);

                AddChildren(ItemsSource);
            }
        }

        protected void AddChildren(IEnumerable items)
        {
            foreach (var item in items)
            {
                var view = ViewFor(item);
                if (view != null)
                {
                    Children.Add(view);
                }
            }
        }

        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is RepeaterView control)
            {
                control.UpdateChildren();
                if (newValue is INotifyCollectionChanged observableCollection)
                {
                    observableCollection.CollectionChanged += (sender, e) =>
                    {
                        switch (e.Action)
                        {
                            case NotifyCollectionChangedAction.Add:
                                control.AddChildren(e.NewItems);
                                break;
                            default:
                                control.UpdateChildren();
                                break;
                        }
                    };
                }
            }
        }

        private static void ResetView(BindableObject bindable, object oldValue, object newValue)
        {
            var repeater = bindable as RepeaterView;
            repeater.UpdateChildren();
        }

        private class DefaultDataTemplate : DataTemplate
        {
            public DefaultDataTemplate()
                : base(GetLabel)
            {
            }

            private static Label GetLabel()
            {
                var label = new Label();

                label.SetBinding(Label.TextProperty, new Binding(".", stringFormat: "{0}"));
                return label;
            }
        }
    }
}
