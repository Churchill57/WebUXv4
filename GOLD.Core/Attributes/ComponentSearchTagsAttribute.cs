using System;

namespace GOLD.Core.Attributes
{
    class ComponentSearchTagsAttribute : Attribute
    {
        public ComponentSearchTagsAttribute(params string[] tags)
        {
            Tags = tags;
        }
        public string[] Tags { get; }
    }
}
