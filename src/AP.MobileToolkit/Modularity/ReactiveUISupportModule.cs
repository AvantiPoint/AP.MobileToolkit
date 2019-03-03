using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace AP.MobileToolkit.Rx
{
    public class ReactiveUISupportModule : IModule, IObserver<Exception>
    {
        private ILogger Logger { get; set; }

        public void OnCompleted() { }

        public void OnError(Exception error)
        {
            Logger.Report(error, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception 'OnError'" }
            });
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Logger = containerProvider.Resolve<ILogger>();
            RxApp.DefaultExceptionHandler = this;
        }

        public void OnNext(Exception value)
        {
            Logger.Report(value, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception 'OnNext'" }
            });
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
