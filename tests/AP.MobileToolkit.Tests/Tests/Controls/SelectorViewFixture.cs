using System.Collections.Generic;
using System.Linq;
using AP.MobileToolkit.Controls;
using AP.MobileToolkit.Tests.Mocks;
using Xamarin.Forms;
using Xunit;
using Xunit.Abstractions;

namespace AP.MobileToolkit.Tests.Tests.Controls
{
    public class SelectorViewFixture : TestBase
    {
        public SelectorViewFixture(ITestOutputHelper testOutputHelper) 
            : base(testOutputHelper)
        {
        }

        [Fact]
        public void EachChildHasTappedGesture()
        {
            var view = new SelectorView
            {
                ItemsSource = GetItems()
            };

            foreach (var child in view.Children)
            {
                _testOutputHelper.WriteLine($"Selectable Item: {((SelectableMock)child.BindingContext).Name}");

                Assert.NotEmpty(child.GestureRecognizers);
                var tappedGesture = (TapGestureRecognizer)child.GestureRecognizers.FirstOrDefault(g => g is TapGestureRecognizer);
                Assert.NotNull(tappedGesture);
                Assert.Equal(child.BindingContext, tappedGesture.CommandParameter);
            }
        }

        [Fact]
        public void AllowOne_OnlyHasSingleSelectedView()
        {
            var view = new SelectorView
            {
                ItemsSource = GetItems(),
                ItemTemplate = new SelectableDataTemplateSelector(),
                AllowMultiple = false
            };

            var selectedItem = view.SelectedItem;
            Assert.NotNull(selectedItem);

            var firstView = view.Children[0];

            Assert.IsType<IsNotSelectedViewMock>(firstView);

            var tappedGesture = firstView.GestureRecognizers.First(g => g is TapGestureRecognizer) as TapGestureRecognizer;
            Assert.NotNull(tappedGesture);
            tappedGesture.SendTapped(firstView);

            Assert.NotEqual(selectedItem, view.SelectedItem);

            Assert.Single(view.Children.Where(v => v is IsSelectedViewMock));
        }

        [Fact]
        public void AllowMultiple_HasMultipleSelectedViews()
        {
            var view = new SelectorView
            {
                AllowMultiple = true,
                ItemsSource = GetItems(),
                ItemTemplate = new SelectableDataTemplateSelector()
            };

            Assert.Null(view.SelectedItem);

            Assert.Single(view.SelectedItems);

            var firstView = view.Children[0];
            Assert.IsType<IsNotSelectedViewMock>(firstView);
            var tappedGesture = firstView.GestureRecognizers.First(g => g is TapGestureRecognizer) as TapGestureRecognizer;
            Assert.NotNull(tappedGesture);

            tappedGesture.SendTapped(firstView);
            Assert.Null(view.SelectedItem);
            Assert.Equal(2, view.SelectedItems.Count);
            Assert.Equal(2, view.Children.Where(v => v is IsSelectedViewMock).Count());

            tappedGesture.SendTapped(firstView);
            Assert.Single(view.SelectedItems);
            Assert.Single(view.Children.Where(v => v is IsSelectedViewMock));
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
                },
                new SelectableMock
                {
                    Name = "D",
                    IsSelected = false
                },
                new SelectableMock
                {
                    Name = "E",
                    IsSelected = false
                }
            };
    }
}
