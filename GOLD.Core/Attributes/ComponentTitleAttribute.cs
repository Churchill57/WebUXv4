﻿using System;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentTitleAttribute : Attribute
    {
        public ComponentTitleAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; }
    }
}
