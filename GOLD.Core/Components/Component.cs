using GOLD.AppExecution.ApiModels;
using GOLD.Core.Attributes;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Components
{
    public abstract class Component : IComponent
    {
        internal static IExecutionManagerInternal executionManager { get; set; }

        internal ExecutionThread executionThread { get; set; }
        //public int RegisteredComponentID { get; set; }
        //public int ExecutionThreadID { get; set; } // The App execution thread (from when the root component was launched until it is done or suspended).
        //public int ComponentExecutionID { get; set; } // Equivalend to TaskId in WebUXv2.
        //public string ComponentExecutionUrl { get; set; }
        //internal protected TXID TXID { get; set; }
        internal protected string ClientRef { get; set; }
        internal protected Dictionary<string, object> State
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
                if (value == null) return;
                foreach (var propertyInfo in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    if (Attribute.IsDefined(propertyInfo, typeof(PropertyIsComponentStateAttribute)))
                    {
                        if (!value.ContainsKey(propertyInfo.Name) || value[propertyInfo.Name] == null)
                        {
                            //propertyInfo.SetValue(this, null);
                        }
                        else
                        {
                            Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                            try
                            {
                                propertyInfo.SetValue(this, Convert.ChangeType(value[propertyInfo.Name], propertyType), null);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    propertyInfo.SetValue(this, Convert.ChangeType(JsonConvert.DeserializeObject(value[propertyInfo.Name].ToString(), propertyType), propertyType), null);
                                }
                                catch (Exception e)
                                {
                                    // https://skrift.io/articles/archive/bulletproof-interface-deserialization-in-jsonnet/
                                    try
                                    {
                                        var jAttr = propertyInfo.GetCustomAttribute<JsonConverterAttribute>();
                                        var jObj = Activator.CreateInstance(jAttr.ConverterType) as JsonConverter;
                                        propertyInfo.SetValue(this, JsonConvert.DeserializeObject(value[propertyInfo.Name].ToString(), propertyType, jObj), null);
                                    }
                                    catch (Exception)
                                    {
                                        throw new Exception($"Failed to deserialize property {this.GetType().Name}.{propertyInfo.Name} of type {propertyInfo.PropertyType.Name}"
                                            + "\n Use a concrete property type or decorate with JsonConverterAttribute to define a custom converter. ");
                                    }
                                }
                            }


                        }
                    }
                }

            }
        }
        public ITXID TXID { get; set; }

        public void Save()
        {
            executionManager.SaveComponentToExecutionThread(this);
        }
        public async Task SaveAsync()
        {
            await executionManager.SaveComponentToExecutionThreadAsync(this);
        }
        public static T Load<T>(string txid) where T : Component, new()
        {
            return executionManager.LoadComponentFromExecutionThread<T>(txid);
        }
        //public static T Load<T>(TXID txid) where T : Component, new()
        //{
        //    return executionManager.LoadComponentFromExecutionThreadAsync<T>(txid).Result;
        //}
        public async static Task<T> LoadAsync<T>(string txid) where T : Component, new()
        {
            return await executionManager.LoadComponentFromExecutionThreadAsync<T>(txid);
        }
        //public async static Task<T> LoadAsync<T>(TXID txid) where T : Component, new()
        //{
        //    return await executionManager.LoadComponentFromExecutionThreadAsync<T>(txid);
        //}

        public async Task<T> UseComponentAsync<T>(string clientRef) where T : Component, new()
        {
            return await executionManager.GetComponentAsync<T>(this, clientRef);
        }

        public async Task<T> UseComponentInterfaceAsync<T>(string clientRef) where T : class//, IComponent
        {
            var proxy = await executionManager.GetComponentInterfaceAsync<T>(this, clientRef);
            return proxy as T;

            //var proxy = CoreFunctions.CreateProxy<T>(typeof(IComponent));
            // TODO: put proxy into thread/get it out from state
            //((IComponent)proxy).TXID = new TXID("1.2");
            //return proxy;
        }

        //if (typeof(T).IsSubclassOf(typeof(Component)))
        //{
        //}
        //if (typeof(T).IsInterface)
        //{
        //}
        //else
        //{
        //}



        //private async Task<T> UseConcreteComponentAsync<T>(string ClientRef)
        //{
        //}

        //private async Task<T> UseComponentInterfaceAsync<T>(string ClientRef)
        //{
        //}

    }
}
