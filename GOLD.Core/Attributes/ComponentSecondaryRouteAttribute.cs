using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentSecondaryRouteAttribute : Attribute
    {
        public ComponentSecondaryRouteAttribute(string route)
        {
            Route = route;
        }
        public string Route { get; }

    }
}
