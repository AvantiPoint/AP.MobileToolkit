using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AP.MobileToolkit.Resources;
using Prism.Modularity;

namespace AP.MobileToolkit.Modularity
{
    internal static class ModuleInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this IModuleInfo moduleInfo, out TAttribute attribute)
            where TAttribute : Attribute
        {
            var moduleType = Type.GetType(moduleInfo.ModuleType);
            attribute = moduleType.GetCustomAttributes<TAttribute>().FirstOrDefault();

            return attribute != null;
        }

        public static bool HasRole(this IModuleInfo moduleInfo, string role)
        {
            if(moduleInfo.HasAttribute(out ModuleRoleAttribute moduleRoleAttribute))
            {
                return moduleRoleAttribute.SupportsRole(role);
            }

            return false;
        }

        public static IEnumerable<IModuleInfo> GetDependentModules(this IModuleInfo moduleInfo, IModuleCatalog moduleCatalog)
        {
            var moduleType = Type.GetType(moduleInfo.ModuleType);
            
            foreach(var name in moduleInfo.DependsOn)
            {
                var dependentModule = moduleCatalog.Modules.FirstOrDefault(m => m.ModuleName == name);

                yield return dependentModule ?? 
                    throw new ModuleNotFoundException(name, string.Format(ToolkitResources.ModuleNotFoundErrorMessage, name));
            }
        }
    }
}
