using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Associates a specific entity context type (e.g. customer, address, policy) with a component property.
    /// The system will automatically try to resolve any such proprties which are null at the time a component is executed.
    /// This may invoke special context searching components.
    /// Equivalent to ComponentInputAttribute in WebUXv2
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyIsContextInputAttribute : Attribute
    {
        public PropertyIsContextInputAttribute(string contextName)
        {
            ContextName = contextName;
        }

        public string ContextName { get; }
    }

}
