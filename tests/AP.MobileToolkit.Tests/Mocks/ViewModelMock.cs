using System;
using System.Threading.Tasks;
using AP.MobileToolkit.Mvvm;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ViewModelMock : APBaseViewModel
    {
        public ViewModelMock()
            : base(new BaseServices(default, default, default, default, default, default))
        {
        }

        public ViewModelMock(BaseServices baseServices)
            : base(baseServices)
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
