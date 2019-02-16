using System;

namespace GOLD.Core.Attributes
{
    class RoleAccessAttribute : Attribute
    {
        public RoleAccessAttribute(params string[] roles)
        {
            Roles = roles;
        }
        public string[] Roles { get; }
    }
}
