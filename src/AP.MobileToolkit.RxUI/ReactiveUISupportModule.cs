using System;
using System.Collections.Generic;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using ReactiveUI;

namespace AP.MobileToolkit.RxUI
{
    public class ReactiveUISupportModule : IModule, IObserver<Exception>
    {
        private ILogger Logger { get; }

        public ReactiveUISupportModule(ILogger logger)
        {
            Logger = logger;
            RxApp.DefaultExceptionHandler = this;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            Logger.Report(error, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception 'OnError'" }
            });
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void OnNext(Exception value)
        {
            Logger.Report(value, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception 'OnNext'" }
            });
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
