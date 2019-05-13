using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AP.MobileToolkit.Modularity.Properties;
using Prism.Modularity;

namespace AP.MobileToolkit.Modularity
{
    public abstract class ModuleManager : IModuleManager
    {
        protected IModuleCatalog ModuleCatalog { get; }

        protected IModuleInitializer ModuleInitializer { get; }

        protected ModuleManager(IModuleCatalog moduleCatalog, IModuleInitializer moduleInitializer)
        {
            ModuleCatalog = moduleCatalog;
            ModuleInitializer = moduleInitializer;
        }

        public event EventHandler<LoadModuleCompletedEventArgs> LoadModuleCompleted;

        public void Run() =>
            LoadModulesWhenAvailable();

        public void LoadModule(string moduleName)
        {
            var modules = ModuleCatalog.Modules.Where(m => m.ModuleName == moduleName);
            if (modules == null || modules.Count() == 0)
            {
                throw new ModuleNotFoundException(moduleName, string.Format(CultureInfo.CurrentCulture, Resources.ModuleNotFound, moduleName));
            }
            else if (modules.Count() > 1)
            {
                throw new DuplicateModuleException(moduleName, string.Format(CultureInfo.CurrentCulture, Resources.DuplicatedModuleInCatalog, moduleName));
            }

            var modulesToLoad = ModuleCatalog.CompleteListWithDependencies(modules);

            LoadModules(modulesToLoad);
        }

        protected void LoadModulesWhenAvailable()
        {
            var whenAvailableModules = ModuleCatalog.Modules.Where(m => m.InitializationMode == InitializationMode.WhenAvailable && m.State == ModuleState.NotStarted);
            if (whenAvailableModules != null)
                LoadModules(whenAvailableModules);
        }

        protected virtual void LoadModules(IEnumerable<IModuleInfo> moduleInfos)
        {
            foreach (var moduleInfo in moduleInfos)
            {
                if (moduleInfo.State == ModuleState.NotStarted)
                {
                    try
                    {
                        moduleInfo.State = ModuleState.Initializing;
                        ModuleInitializer.Initialize(moduleInfo);
                        moduleInfo.State = ModuleState.Initialized;
                        RaiseLoadModuleCompleted(moduleInfo);
                    }
                    catch (Exception ex)
                    {
                        RaiseLoadModuleCompleted(moduleInfo, ex);
                    }
                }
            }
        }

        protected void RaiseLoadModuleCompleted(IModuleInfo moduleInfo, Exception ex = null)
        {
            LoadModuleCompleted?.Invoke(this, new LoadModuleCompletedEventArgs(moduleInfo, ex));
        }
    }
}
