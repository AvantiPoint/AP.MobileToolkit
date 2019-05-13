using System.Collections;
using System.Collections.Generic;
using AP.CrossPlatform.Auth;
using AP.CrossPlatform.Auth.Events;
using Prism.Events;
using Prism.Modularity;

namespace AP.MobileToolkit.Modularity
{
    public abstract class AuthenticatedUserAwareModuleManager : ModuleManager
    {
        public AuthenticatedUserAwareModuleManager(IModuleInitializer moduleInitializer, IModuleCatalog moduleCatalog, IEventAggregator eventAggregator)
            : base(moduleCatalog, moduleInitializer)
        {
            eventAggregator.GetEvent<AuthenticatedUserEvent>().Subscribe(OnAuthenticatedUserEventPublished);
        }

        protected abstract void OnAuthenticatedUserEventPublished(IUser user);

        protected class UniqueModuleInfoCollection : IEnumerable<IModuleInfo>
        {
            private List<IModuleInfo> Modules => new List<IModuleInfo>();

            public void Add(IModuleInfo module)
            {
                if (!Modules.Contains(module))
                {
                    Modules.Add(module);
                }
            }

            public void AddRange(IEnumerable<IModuleInfo> modules)
            {
                foreach (var module in modules)
                {
                    Add(module);
                }
            }

            public IEnumerator<IModuleInfo> GetEnumerator() =>
                Modules.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                GetEnumerator();
        }
    }
}
