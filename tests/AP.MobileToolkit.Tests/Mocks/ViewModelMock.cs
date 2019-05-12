using System;
using System.Threading.Tasks;
using AP.MobileToolkit.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ViewModelMock : ViewModelBase
    {
        public ViewModelMock()
            : base(null, null, null)
        {
        }

        public ViewModelMock(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }

        public string CorrelationId { get; private set; }

        protected override Task DisplayAlertForException(Exception ex, string correlationId)
        {
            CorrelationId = correlationId;
            return base.DisplayAlertForException(ex, correlationId);
        }
    }
}
