using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Indicates the public interface which uniquely identifies this component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentInterfaceAttribute : Attribute
    {
        public ComponentInterfaceAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}
