using System.Collections.Generic;
using AP.CrossPlatform.Collections;
using Prism.Commands;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace ToolkitDemo.ViewModels
{
    public class SwipeCardViewPageViewModel : DemoPageViewModelBase
    {
        public SwipeCardViewPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
            OnSwipe = new DelegateCommand<string>(ExecuteSwipeCommand);
            OnDrag = new DelegateCommand<string>(ExecuteDragCommand);

            ItemList = new ObservableRangeCollection<string>()
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };
        }

        public IEnumerable<string> ItemList { get; }

        public DelegateCommand<string> OnSwipe;

        public DelegateCommand<string> OnDrag;

        private void ExecuteSwipeCommand(string item)
        {
            // Do nothing;
        }

        private void ExecuteDragCommand(string item)
        {
            // Do nothing;
        }
    }
}
