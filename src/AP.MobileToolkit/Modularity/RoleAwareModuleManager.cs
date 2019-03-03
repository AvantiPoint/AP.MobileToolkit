using System;
using System.Collections.Generic;
using System.Linq;
using AP.MobileToolkit.Authentication;
using Prism.Events;
using Prism.Modularity;

namespace AP.MobileToolkit.Modularity
{
    public class RoleAwareModuleManager : AuthenticatedUserAwareModuleManager
    {
        public RoleAwareModuleManager(IModuleInitializer moduleInitializer, IModuleCatalog moduleCatalog, IEventAggregator eventAggregator)
            : base(moduleInitializer, moduleCatalog, eventAggregator)
        {
            
        }

        protected override void OnAuthenticatedUserEventPublished(IUser user)
        {
            var modules = new UniqueModuleInfoCollection();
            foreach(var role in GetRoles(user))
            {
                modules.AddRange(ModuleCatalog.Modules.Where(m => m.HasRole(role)));
            }

            LoadModules(modules);
        }

        protected virtual IEnumerable<string> GetRoles(IUser user)
        {
            var key = user.Keys.FirstOrDefault(k => k.Equals("role", StringComparison.InvariantCultureIgnoreCase) || k == "extension_Role");
            if(!string.IsNullOrWhiteSpace(key))
            {
                return user[key].Split(',');
            }

            return Array.Empty<string>();
        }
    }
}
