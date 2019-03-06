using GOLD.Core.Interfaces;
using ImpromptuInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace GOLD
{
    public static class CoreFunctions
    {
        public static Type GetType(string typename)
        {
            var result = (
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                select t
            ).FirstOrDefault(t => t.FullName == typename);
            return result;
        }

        public static T CreateProxy<T>(Type type) where T : class
        {
            return GetProxy(typeof(T)).ActLike<T>(type);
        }

        public static T CreateProxy<T>() where T : class
        {
            return GetProxy(typeof(T)).ActLike<T>();
        }

        public static T CreateProxy<T>(string json) where T : class
        {
            var deserializedOriginal = JsonConvert.DeserializeObject(json);
            return deserializedOriginal.ActLike<T>();
        }

        public static T CreateProxy<T>(string json, Type type) where T : class
        {
            var deserializedOriginal = JsonConvert.DeserializeObject(json);
            return deserializedOriginal.ActLike<T>(type);
        }

        private static ExpandoObject GetProxy(Type type)
        {
            var proxy = new ExpandoObject();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.MemberType != MemberTypes.Property) continue;
                if (!prop.CanWrite) continue;

                object defaultValue = null;
                if (prop.PropertyType.FullName.StartsWith("System"))
                {
                    if (prop.PropertyType.IsValueType && Nullable.GetUnderlyingType(prop.PropertyType) == null)
                    {
                        defaultValue = Activator.CreateInstance(prop.PropertyType);
                    }
                }
                else
                {
                    if (prop.PropertyType.IsInterface)
                    {
                        defaultValue = Impromptu.DynamicActLike(GetProxy(prop.PropertyType), prop.PropertyType);
                    }
                    else
                    {
                        defaultValue = Activator.CreateInstance(prop.PropertyType);
                    }
                }
                ((IDictionary<String, object>)proxy)[prop.Name] = defaultValue;
            }
            return proxy;
        }

        public static string ProxyAsJson(object proxy)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize }; // Avoid nested expando references.
            var proxyJson = JsonConvert.SerializeObject(proxy, settings);
            var jObject = JObject.Parse(proxyJson);
            var jOutput = KeepRelevantTokens(jObject);
            var cleansedJson = jOutput.ToString(Formatting.None);
            // To compare examples of proxyJson and cleansedJson, see files Example1a.json and Example1b.json respecively
            return cleansedJson;
        }

        private static JObject KeepRelevantTokens(JToken containerToken)
        {
            var jOutput = new JObject();
            KeepRelevantTokens(containerToken, false, jOutput);
            return jOutput;
        }

        private static void KeepRelevantTokens(JToken containerToken, bool captureChildren, JToken output)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (captureChildren)
                    {
                        if (child.Value.Type == JTokenType.Object)
                        {
                            ((JObject)output).Add(child.Name,new JObject());
                            KeepRelevantTokens(child.Value, (child.Value.SelectToken("Original") == null), ((JProperty)((JObject)output).Last).Value);
                        }
                        else
                        {
                            ((JObject)output).Add(child);
                        }
                    }
                    else
                    {
                        KeepRelevantTokens(child.Value, (child.Name == "Original"), output);
                    }
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    KeepRelevantTokens(child, false, output);
                }
            }
        }


        //public void SerializeInterfaceProxy()
        //{
        //    // Create a proxy object which implements the interface IParametersTest.
        //    var proxy = CoreFunctions.CreateProxy<IUxCustomerAdvSearchCriteria>();

        //    // Set any properties on the proxy as required.
        //    proxy.ShowSwitchToBasic = true;
        //    proxy.Criteria.Age = 21;
        //    proxy.Criteria.FirstName = "John";
        //    proxy.Criteria.Gender = Gender.Male;
        //    proxy.Criteria.NINO = "AB123456C";
        //    proxy.Criteria.PostCode = "CR3 7DH";
        //    proxy.Criteria.Surname = "Smith";
        //    proxy.Criteria.Town = "Croydon";

        //    proxy.Criterias = new List<ICustomerAdvancedSearchCriteria>();
        //    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 11 });
        //    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 22 });
        //    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 22 });

        //    var jsonToPersist = CoreFunctions.ProxyAsJson(proxy);

        //    var rehydratedProxy = CoreFunctions.CreateProxy<IUxCustomerAdvSearchCriteria>(jsonToPersist);

        //    // Set any properties on the proxy as required. 
        //    rehydratedProxy.Criteria.Age = 19;
        //    rehydratedProxy.ShowSwitchToBasic = false;
        //    rehydratedProxy.Criteria.NINO = "XY123456Z";
        //    rehydratedProxy.Criterias.ElementAt(0).Age = 999;
        //    rehydratedProxy.Criterias.ElementAt(1).Age = 888;
        //    rehydratedProxy.Criterias.ElementAt(2).Age = 777;


        //}

    }
}
