using AP.CrossPlatform;
using AP.CrossPlatform.Collections;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    public partial class SelectorView
    {
        public static readonly BindableProperty AllowMultipleProperty =
            BindableProperty.Create(nameof(AllowMultiple), typeof(bool), typeof(SelectorView), false, propertyChanged: OnAllowMultipleChanged);

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(SelectorView), null, propertyChanged: OnSelectedItemChanged);

        public static readonly BindableProperty SelectedItemsProperty =
            BindableProperty.Create(nameof(SelectedItems), typeof(IList), typeof(SelectorView), new ObservableRangeCollection(), propertyChanged: OnSelectedItemsChanged);

        private static void OnAllowMultipleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectorView = bindable as SelectorView;

            if(!selectorView.AllowMultiple && selectorView.SelectedItems.NullableCount() > 1)
            {
                for(int i = 0; i < selectorView.SelectedItems.Count(); i++)
                {
                    var item = selectorView.SelectedItems[i];
                    if(i == 0)
                    {
                        selectorView.SelectedItem = item;
                        continue;
                    }

                    if(item is ISelectable selectable)
                    {
                        selectable.IsSelected = false;
                    }
                }
            }
            else if(selectorView.AllowMultiple && selectorView.SelectedItem != null)
            {
                selectorView.SelectedItems.Add(selectorView.SelectedItem);
            }

            if(selectorView.AllowMultiple)
            {
                selectorView.SelectedItem = null;
            }
            else
            {
                selectorView.SelectedItems.Clear();
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectorView = bindable as SelectorView;
        }

        private static void OnSelectedItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var selectorView = bindable as SelectorView;
        }
    }
}
