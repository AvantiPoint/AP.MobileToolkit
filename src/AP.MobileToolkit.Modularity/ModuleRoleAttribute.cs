using System;
using System.Linq;

namespace AP.MobileToolkit.Modularity
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ModuleRoleAttribute : Attribute
    {
        private string[] Roles { get; }

        public ModuleRoleAttribute(params string[] roles) => Roles = roles;

        public bool SupportsRole(string role) =>
            Roles.Any(r => r.Equals(role, StringComparison.InvariantCultureIgnoreCase));
    }
}
