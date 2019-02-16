using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SecondaryRouteAttribute : Attribute
    {
        public SecondaryRouteAttribute(string route)
        {
            Route = route;
        }
        public string Route { get; }

    }
}
