using System;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Relates to the appropriate MVC controller action method for starting a new instance of the component.
    /// Typically in the form @"controller\action" unless a specific RouteAttribute has been applied to the controller and/or method.
    /// </summary>
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
