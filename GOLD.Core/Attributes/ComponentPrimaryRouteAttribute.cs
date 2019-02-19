using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentPrimaryRouteAttribute : Attribute
    {
        public ComponentPrimaryRouteAttribute(string route)
        {
            Route = route;
        }
        public string Route { get; }

    }
}
