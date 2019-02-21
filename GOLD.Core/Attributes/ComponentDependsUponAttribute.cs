using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Indicates which other components that this component depends upon.
    /// Used for dependency meta data when a component is registered.
    /// Child component invocation will fail if dependeny is not declared by this attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentDependsUponAttribute : Attribute
    {
        public ComponentDependsUponAttribute(params Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; }
    }
}
