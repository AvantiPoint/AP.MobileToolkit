using System;
using AP.CrossPlatform;
using Xamarin.Forms;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class SelectableDataTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is ISelectable selectable)
            {
                return new DataTemplate(() =>
                {
                    if (selectable.IsSelected)
                        return new IsSelectedViewMock { BindingContext = item };

                    return new IsNotSelectedViewMock { BindingContext = item };
                });
            }

            throw new Exception("The item must be a selectable item");
        }
    }
}
