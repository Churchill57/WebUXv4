using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentSearchTagsAttribute : Attribute
    {
        public ComponentSearchTagsAttribute(params string[] tags)
        {
            Tags = tags;
        }
        public string[] Tags { get; }
    }
}
