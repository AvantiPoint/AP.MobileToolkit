using System.Collections.Generic;
using System.Linq;
using AP.CrossPlatform;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Tests.Mocks;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Controls
{
    public class RepeaterViewFixture : TestBase
    {
        public RepeaterViewFixture(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        private List<SelectableMock> GetItems() =>
            new List<SelectableMock>
            {
                new SelectableMock
                {
                    Name = "A",
                    IsSelected = false
                },
                new SelectableMock
                {
                    Name = "B",
                    IsSelected = true
                },
                new SelectableMock
                {
                    Name = "C",
                    IsSelected = false
                }
            };

        [Fact]
        public void ViewHasThreeChildren()
        {
            var items = GetItems();
            var repeaterView = new RepeaterView
            {
                ItemsSource = items
            };

            Assert.Equal(items.Count(), repeaterView.Children.Count);
        }

        [Fact]
        public void ChildrenHaveBindingContext()
        {
            var items = GetItems();
            var repeaterView = new RepeaterView
            {
                ItemsSource = items
            };

            for (var i = 0; i < items.Count(); i++)
            {
                var child = repeaterView.Children[i];
                var context = items[i];

                Assert.NotNull(child);
                Assert.NotNull(context);

                Assert.Equal(context, child.BindingContext);
            }
        }

        [Fact]
        public void ChildrenAreSetByDataTemplateSelector()
        {
            var items = GetItems();
            var repeaterView = new RepeaterView
            {
                ItemsSource = items,
                ItemTemplate = new SelectableDataTemplateSelector()
            };

            for (var i = 0; i < items.Count(); i++)
            {
                var child = repeaterView.Children[i];
                var context = child.BindingContext as ISelectable;

                Assert.NotNull(child);
                Assert.NotNull(context);

                if (context.IsSelected)
                {
                    Assert.IsType<IsSelectedViewMock>(child);
                }
                else
                {
                    Assert.IsType<IsNotSelectedViewMock>(child);
                }
            }
        }
    }
}
