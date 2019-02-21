using System;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Relates to the appropriate MVC controller action method for display the component in the secondary Portal iFrame.
    /// Typically in the form @"controller\action" unless a specific RouteAttribute has been applied to the controller and/or method.
    /// </summary>
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
