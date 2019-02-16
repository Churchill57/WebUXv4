using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PrimaryRouteAttribute : Attribute
    {
        public PrimaryRouteAttribute(string route)
        {
            Route = route;
        }
        public string Route { get; }

    }
}
