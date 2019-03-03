using AP.MobileToolkit.Mvvm;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ViewModelMock : ViewModelBase
    {
        public ViewModelMock() : base(null, null, null) { }

        public ViewModelMock(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger) 
            : base(navigationService, pageDialogService, logger)
        {
        }
    }
}
