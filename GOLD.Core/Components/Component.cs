using GOLD.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Components
{
    public abstract class Component
    {
        //public int RegisteredComponentID { get; set; }
        //public int ExecutionThreadID { get; set; } // The App execution thread (from when the root component was launched until it is done or suspended).
        //public int ComponentExecutionID { get; set; } // Equivalend to TaskId in WebUXv2.
        //public string ComponentExecutionUrl { get; set; }
        public string ClientRef { get; set; }
        public Dictionary<string, object> State
        {
            get
            {
                var state = new Dictionary<string, object>();
                foreach (var propertyInfo in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(PropertyIsComponentStateAttribute)))
                    {
                        state.Add(propertyInfo.Name, propertyInfo.GetValue(this));
                    }
                }
                return state;
            }
            set
            {
                // TODO: Set Component State properties
                //foreach (var propertyInfo in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                //{
                //    var attr = propertyInfo.GetCustomAttributes(typeof(ComponentStateAttribute), false);
                //    if (attr.Length == 1)
                //    {
                //        if (!value.ContainsKey(propertyInfo.Name) || value[propertyInfo.Name] == null)
                //        {
                //            //propertyInfo.SetValue(this, null);
                //        }
                //        else
                //        {
                //            Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                //            try
                //            {
                //                propertyInfo.SetValue(this, Convert.ChangeType(value[propertyInfo.Name], propertyType), null);
                //            }
                //            catch (Exception)
                //            {
                //                try
                //                {
                //                    propertyInfo.SetValue(this, Convert.ChangeType(JsonConvert.DeserializeObject(value[propertyInfo.Name].ToString(), propertyType), propertyType), null);
                //                }
                //                catch (Exception e)
                //                {
                //                    // https://skrift.io/articles/archive/bulletproof-interface-deserialization-in-jsonnet/
                //                    try
                //                    {
                //                        var jAttr = propertyInfo.GetCustomAttribute<JsonConverterAttribute>();
                //                        var jObj = Activator.CreateInstance(jAttr.ConverterType) as JsonConverter;
                //                        propertyInfo.SetValue(this, JsonConvert.DeserializeObject(value[propertyInfo.Name].ToString(), propertyType, jObj), null);
                //                    }
                //                    catch (Exception)
                //                    {
                //                        throw new Exception($"Failed to deserialize property {this.GetType().Name}.{propertyInfo.Name} of type {propertyInfo.PropertyType.Name}"
                //                            + "\n Use a concrete property type or decorate with JsonConverterAttribute to define a custom converter. ");
                //                    }
                //                }
                //            }


                //        }
                //    }
                //}

            }

        }
    }
}
