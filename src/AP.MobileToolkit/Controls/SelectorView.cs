using AP.CrossPlatform;
using AP.CrossPlatform.Collections;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public partial class SelectorView : RepeaterView
    {
        public bool AllowMultiple
        {
            get => (bool)GetValue(AllowMultipleProperty);
            set => SetValue(AllowMultipleProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        private ICommand ItemTappedCommand => new Command<object>(OnItemTapped);

        private void OnItemTapped(object item)
        {
            if(item is ISelectable selectable)
            {
                var index = Children.IndexOf(Children.FirstOrDefault(v => v.BindingContext == item));
                selectable.IsSelected = !selectable.IsSelected;
                Children[index] = ViewFor(selectable);

                if(selectable.IsSelected)
                {
                    if(AllowMultiple)
                    {
                        SelectedItems.Add(selectable);
                    }
                    else
                    {
                        if(SelectedItem is ISelectable currentlySelected)
                        {
                            currentlySelected.IsSelected = false;
                            var currentlySelectedIndex = Children.IndexOf(Children.FirstOrDefault(v => v.BindingContext == currentlySelected));
                            Children[currentlySelectedIndex] = ViewFor(currentlySelected);
                        }
                        SelectedItem = item;
                    }
                }
                else
                {
                    if(AllowMultiple)
                    {
                        SelectedItems.Remove(item);
                    }
                    else
                    {
                        SelectedItem = null;
                    }
                }
            }
        }

        protected override View ViewFor(object item)
        {
            var view = base.ViewFor(item);

            view.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ItemTappedCommand,
                CommandParameter = item
            });

            return view;
        }

        private void UpdateView(object item)
        {
            int index = Children.IndexOf(Children.First(v => v.BindingContext == item));
            Children[index] = ViewFor(item);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch(propertyName)
            {
                case nameof(ItemsSource):
                    if(ItemsSource.Cast<object>().Any(i => i is ISelectable selectable))
                    {
                        if(AllowMultiple)
                        {
                            HandleAllowMultiple();
                        }
                        else
                        {
                            HandleAllowSingle();
                        }
                    }
                    break;
            }
        }

        private void HandleAllowMultiple()
        {
            if(SelectedItems == null)
            {
                SelectedItems = new ObservableRangeCollection();
            }

            var items = ItemsSource.Cast<object>().Where(i => i is ISelectable selectable && selectable.IsSelected);

            if (SelectedItems is ObservableRangeCollection<object> range)
            {
                range.ReplaceRange(items);
            }
            else
            {
                SelectedItems.Clear();
                foreach (var item in items)
                {
                    SelectedItems.Add(item);
                }
            }
        }

        private void HandleAllowSingle()
        {
            var selected = ItemsSource.Cast<object>().Where(i => i is ISelectable selectable && selectable.IsSelected).Cast<ISelectable>();

            foreach(var item in selected)
            {
                if (SelectedItem == null)
                    SelectedItem = item;

                if (SelectedItem != item)
                    item.IsSelected = false;
            }
        }
    }
}
