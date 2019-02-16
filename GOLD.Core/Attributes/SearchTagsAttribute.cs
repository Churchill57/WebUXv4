using System;

namespace GOLD.Core.Attributes
{
    class SearchTagsAttribute : Attribute
    {
        public SearchTagsAttribute(params string[] tags)
        {
            Tags = tags;
        }
        public string[] Tags { get; }
    }
}
