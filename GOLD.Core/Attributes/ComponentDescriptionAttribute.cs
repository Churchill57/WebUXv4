using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentDescriptionAttribute : Attribute
    {
        public ComponentDescriptionAttribute(string description)
        {
            Description = description;
        }
        public string Description { get; }
    }
}
