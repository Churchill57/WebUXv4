using System;

namespace GOLD.Core.Attributes
{
    class ComponentRoleAccessAttribute : Attribute
    {
        public ComponentRoleAccessAttribute(params string[] roles)
        {
            Roles = roles;
        }
        public string[] Roles { get; }
    }
}
