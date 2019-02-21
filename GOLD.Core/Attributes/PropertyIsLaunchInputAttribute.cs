using System;

namespace GOLD.Core.Attributes
{
    /// <summary>
    /// Indicates if a property of a lu or ux can be specified at the time a user adds an app to their own list of apps.
    /// by default the UI displays the c# property name (using reflection) but may be overridden with a friendlier display name.
    /// The data type is string by default, but may be some other type to trigger validation of the value entered.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class PropertyIsLaunchInputAttribute : Attribute
    {
        public PropertyIsLaunchInputAttribute(string displayName, string dataType)
        {
            DisplayName = displayName;
            DataType = dataType;
        }

        public string DisplayName { get; }
        public string DataType { get; }
    }
}
