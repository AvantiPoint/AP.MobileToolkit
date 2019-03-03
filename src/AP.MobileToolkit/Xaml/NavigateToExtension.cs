using System;
using System.Threading.Tasks;
using AP.MobileToolkit.Extensions;
using AP.MobileToolkit.Resources;
using Prism;
using Prism.Ioc;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;

namespace AP.MobileToolkit.Xaml
{
    public class NavigateToExtension : Prism.Navigation.Xaml.NavigateToExtension
    {
        protected override async Task HandleNavigation(INavigationParameters parameters, INavigationService navigationService)
        {
            var result = await navigationService.NavigateAsync(Name, parameters);

            if(result.Exception != null)
            {
                var correlationId = Guid.NewGuid().ToString();
                var errorParameters = parameters.ToErrorParameters(Name);
                errorParameters.Add("CorrelationId", correlationId);
                GetService<ILogger>().Report(result.Exception, errorParameters);

                await GetService<IPageDialogService>().DisplayAlertAsync(
                    ToolkitResources.Error,
                    string.Format(ToolkitResources.AlertErrorMessageTemplate, result.Exception.GetType().Name, correlationId),
                    ToolkitResources.Ok);
            }
        }

        private T GetService<T>() => PrismApplicationBase.Current.Container.Resolve<T>();
    }
}
