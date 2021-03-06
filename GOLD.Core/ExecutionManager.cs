﻿using GOLD.AppExecution.ApiClient;
using GOLD.AppExecution.ApiModels;
using GOLD.AppRegister.ApiClient;
using GOLD.AppRegister.ApiModels;
using GOLD.Core.Attributes;
using GOLD.Core.Components;
using GOLD.Core.Enums;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GOLD.Core.Outcomes;

namespace GOLD.Core
{
    public class ExecutionManager : IExecutionManager, IExecutionManagerInternal
    {
        public ExecutionManager()
        {
            Component.executionManager = this;
        }

        private static readonly Lazy<HttpClient> httpClientForAppRegister = new Lazy<HttpClient>(() => {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["GOLD.AppRegister.WebApi.BaseURL"])
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        });

        private static readonly Lazy<AppRegisterApiClient> lazyAppRegisterApiClient = new Lazy<AppRegisterApiClient>(() => new AppRegisterApiClient(httpClientForAppRegister));
        private static AppRegisterApiClient AppRegisterApiClient => lazyAppRegisterApiClient.Value;


        private static readonly Lazy<HttpClient> httpClientForAppExecution = new Lazy<HttpClient>(() =>
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["GOLD.AppExecution.WebApi.BaseURL"])
            };
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        });

        private static readonly Lazy<AppExecutionApiClient> lazyAppExecutionApiClient = new Lazy<AppExecutionApiClient>(() => new AppExecutionApiClient(httpClientForAppExecution));
        private static AppExecutionApiClient AppExecutionApiClient => lazyAppExecutionApiClient.Value;

        /// <summary>
        /// Returns URL of component instance after initiating a new execution thread.
        /// </summary>
        /// <param name="componentInterface"></param>
        /// <returns></returns>
        public async Task<string> RedirectLaunchAppAsync(string componentInterfaceFullname, string returnUrl) // Potentially more params to come inc. returnTaskId, forceContextSearch, etc
        {
            // Get info about the registered component.
            var registeredComponent = await AppRegisterApiClient.GetComponentByInterfaceFullName(componentInterfaceFullname);
            if (registeredComponent == null) throw new Exception($"Component interface '{componentInterfaceFullname}' is not registered.");
            var componentInterfaceName = componentInterfaceFullname.Split('.').LastOrDefault();

            var launcher = new LuLauncher() { ClientRef = componentInterfaceFullname,  ReturnUrl = returnUrl };

            var thread = new ExecutionThread()
            {
                ExecutingComponents = new List<ExecutingComponent>()
                {
                    new ExecutingComponent()
                    {
                        ExecutingID = 0, // Starts at 0 and will later be incremented for each additional component instance executing within the thread.
                        InterfaceFullname = typeof(LuLauncher).FullName, // a system Launcher Component is always the entry point to a thread
                        TypeFullname = typeof(LuLauncher).FullName,
                        //URL = "?", // Relevant for LuLauncher??
                        Breadcrumb = "LuLauncher(0)", // Includes ExecutingID.
                        ParentExecutingID = 0, // No parent for entry point launcher
                        ClientRef = launcher.ClientRef,
                        State = launcher.State,
                        Title = registeredComponent.Title
                    },
                    new ExecutingComponent() // The root component (i.e. the component launched for execution).
                    {
                        ExecutingID = 1, // Starts at 0 and is incremented for each component executed within the thread.
                        InterfaceFullname = componentInterfaceFullname,
                        //TypeFullname = "?", unknown until instantiated.
                        URL = $"{registeredComponent.DomainName}/{registeredComponent.PrimaryAppRoute}",
                        Breadcrumb = $"LuLauncher(0)/{componentInterfaceName}(1)",
                        ClientRef = componentInterfaceName, 
                        ParentExecutingID = 0, // LuLauncher acts as parent to root component.
                        State = null,
                        Title = registeredComponent.Title
                    }
                },
                ComponentExecutingID = 1, // Execution starts at the root component.
                ExecutingComponentTitle = registeredComponent.Title,
                ExecutionStatus = (int)LogicalUnitStatusEnum.Initialised,
                LaunchInputs = null,
                LockDateTime = DateTime.Now,
                LockUserID = Guid.NewGuid().ToString(), // TODO: Determine user ID
                LockUserName = "Anonymous",
                RootComponentTitle  = registeredComponent.Title //JH ,
                //JHPendingOutcome = null
            };

            var persistedThread = await AppExecutionApiClient.SaveNewExecutionThreadAsync(thread);

            return await RedirectResumeExecutionThreadAsync(persistedThread.ID);
        }


        public async Task<string> RedirectResumeExecutionThreadAsync(int tid)
        {
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(tid);
            return await RedirectResumeExecutionThreadAsync(executionThread);
        }

        public async Task<string> RedirectResumeExecutionThreadAsync(ExecutionThread executionThread)
        {
            var txid = new TXID(executionThread.ID, executionThread.ComponentExecutingID).ToString();
            var componentExecuting = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == executionThread.ComponentExecutingID);
            componentExecuting.URL = $"{componentExecuting.URL}/{txid}";
            executionThread.ExecutionStatus = (int)LogicalUnitStatusEnum.Started;
            executionThread = await AppExecutionApiClient.SaveExecutionThreadAsync(executionThread);
            return componentExecuting.URL;
        }

        //public async Task<ExecutionThread> LoadExecutionThreadAsync(int tid)
        //{
        //    return await AppExecution.LoadExecutionThreadAsync(tid);
        //}

        public async Task<IComponent> LoadComponentAsync(string txid)
        {
            var txid2 = new TXID(txid);
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(txid2.tid);
            return ExtractComponentFromExecutionThread(executionThread, txid2.xid);

            //return await LoadComponentFromExecutionThreadAsync<T>(new TXID(txid));
        }

        public async Task<T> LoadComponentFromExecutionThreadAsync<T>(string txid) where T : Component, new()
        {
            var txid2 = new TXID(txid);
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(txid2.tid);
            return ExtractComponentFromExecutionThread<T>(executionThread, txid2.xid);

            //return await LoadComponentFromExecutionThreadAsync<T>(new TXID(txid));
        }

        public T LoadComponentFromExecutionThread<T>(string txid) where T : Component, new()
        {
            var txid2 = new TXID(txid);
            var executionThread = AppExecutionApiClient.LoadExecutionThread(txid2.tid);
            return ExtractComponentFromExecutionThread<T>(executionThread, txid2.xid);

            //return await LoadComponentFromExecutionThreadAsync<T>(new TXID(txid));
        }

        public async Task<T> LoadComponentInterfaceFromExecutionThreadAsync<T>(string txid) where T : class
        {
            var txid2 = new TXID(txid);
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(txid2.tid);
            return ExtractComponentInterfaceFromExecutionThread<T>(executionThread, txid2.xid);

            //return await LoadComponentFromExecutionThreadAsync<T>(new TXID(txid));
        }


        //public async Task<T> LoadComponentFromExecutionThreadAsync<T>(TXID txid) where T : Component, new()
        //{
        //    var executionThread = await AppExecution.LoadExecutionThreadAsync(txid.tid);
        //    return ExtractComponentFromExecutionThread<T>(executionThread, txid.xid);
        //}

        private T ExtractComponentFromExecutionThread<T>(ExecutionThread executionThread, int xid) where T : Component, new()
        {
            var t = new T();
            var component = t as Component;
            if (component == null) return null;

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == xid);

            if (string.IsNullOrEmpty(executingComponent.TypeFullname)) executingComponent.TypeFullname = typeof(T).FullName;

            //Component.executionManager = this; // See constructor
            component.executionThread = executionThread;
            component.TXID = new TXID(executionThread.ID, xid);
            component.ClientRef = executingComponent.ClientRef;
            component.State = executingComponent.State;

            return t;
        }

        private T ExtractComponentInterfaceFromExecutionThread<T>(ExecutionThread executionThread, int xid) where T : class
        {
            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == xid);

            T proxy;
            IComponent component;
            if (executingComponent == null)
            {
                proxy = CoreFunctions.CreateProxy<T>(typeof(IComponent));
                component = proxy as IComponent;
                component.TXID = new TXID(executionThread.ID, executingComponent.ExecutingID);
            }
            else
            {
                var stateJson = JsonConvert.SerializeObject(executingComponent.State);
                proxy = CoreFunctions.CreateProxy<T>(stateJson, typeof(IComponent));
                component = proxy as IComponent;
            }

            return proxy;
        }


        public async Task SaveComponentToExecutionThreadAsync(Component component)
        {
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(component.TXID.tid);
            await SaveComponentToExecutionThreadAsync(component, executionThread);
        }
        public void SaveComponentToExecutionThread(Component component)
        {
            var executionThread = AppExecutionApiClient.LoadExecutionThread(component.TXID.tid);
            SaveComponentToExecutionThread(component, executionThread);
        }

        private async Task SaveComponentToExecutionThreadAsync(Component component, ExecutionThread executionThread)
        {
            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == component.TXID.xid);
            executingComponent.State = component.State;
            executionThread = await AppExecutionApiClient.SaveExecutionThreadAsync(executionThread);
        }

        private void SaveComponentToExecutionThread(Component component, ExecutionThread executionThread)
        {
            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == component.TXID.xid);
            executingComponent.State = component.State;
            executionThread = AppExecutionApiClient.SaveExecutionThread(executionThread);
        }

        internal T ExtractComponentFromExecutionThread<T>(Component parentComponent, string clientRef) where T : new()
        {
            var t = new T();
            var component = t as Component;
            if (component == null) return default(T);

            var executionThread = parentComponent.executionThread;

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ParentExecutingID == parentComponent.TXID.xid && e.ClientRef == clientRef
            );

            //Component.executionManager = this; // See constructor
            component.executionThread = executionThread;
            component.TXID = new TXID(executionThread.ID, executingComponent.ExecutingID);
            component.ClientRef = executingComponent.ClientRef;
            component.State = executingComponent.State;

            return t;
        }

       public async Task<T> GetComponentAsync<T>(Component parentComponent, string clientRef) where T : Component, new()
        {
            var t = new T();
            var component = t as Component;
            if (component == null) return null;

            var executionThread = parentComponent.executionThread;

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ParentExecutingID == parentComponent.TXID.xid && e.ClientRef == clientRef
            );

            var componentInterfaceFullname = typeof(T).GetCustomAttribute<ComponentInterfaceAttribute>()?.Type.FullName;
            // TODO: Cater for unregistered components!
            var registeredComponent = await AppRegisterApiClient.GetComponentByInterfaceFullName(componentInterfaceFullname);
            if (registeredComponent == null) throw new Exception($"Component interface '{componentInterfaceFullname}' is not registered.");


            if (executingComponent == null)
            {
                var parentExecutingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == parentComponent.TXID.xid);
                if (parentExecutingComponent == null) throw new Exception($"Exececuting component with id'{parentComponent.TXID.xid}' not found in execution thread with id'{parentComponent.TXID.tid}.");

                if (registeredComponent == null) throw new Exception($"Component interface '{componentInterfaceFullname}' is not registered.");
                var componentInterfaceName = componentInterfaceFullname.Split('.').LastOrDefault();

                int nextExecutingID = executionThread.ExecutingComponents.Count(); // Zero based.

                var nextTXID = new TXID(executionThread.ID, nextExecutingID).ToString();
                executingComponent = new ExecutingComponent()
                {
                    ExecutingID = nextExecutingID,
                    InterfaceFullname = componentInterfaceFullname,
                    TypeFullname = t.GetType().FullName,
                    URL = $"{registeredComponent.DomainName}/{registeredComponent.PrimaryAppRoute}/{nextTXID}",
                    Breadcrumb = $"{parentExecutingComponent.Breadcrumb}/{componentInterfaceName}({nextExecutingID})",
                    ClientRef = clientRef,
                    ParentExecutingID = parentExecutingComponent.ExecutingID,
                    State = null,
                    Title = registeredComponent.Title
                };
                executionThread.ExecutingComponents.Add(executingComponent);
                executionThread.ComponentExecutingID = nextExecutingID;
                executionThread.ExecutingComponentTitle = registeredComponent.Title;
            }

            component.executionThread = executionThread;
            component.TXID = new TXID(executionThread.ID, executingComponent.ExecutingID);
            component.ClientRef = executingComponent.ClientRef;
            component.State = executingComponent.State;

            return t;
        }

        public async Task<IComponent> GetComponentInterfaceAsync<T>(Component parentComponent, string clientRef) where T : class//, IComponent
        {
            var executionThread = parentComponent.executionThread;

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ParentExecutingID == parentComponent.TXID.xid && e.ClientRef == clientRef
            );

            var childComponentWithClientRefNotInThread = (executingComponent == null);
            if (childComponentWithClientRefNotInThread)
            {
                var componentInterfaceFullname = typeof(T).FullName;
                var registeredComponent = await AppRegisterApiClient.GetComponentByInterfaceFullName(componentInterfaceFullname);
                if (registeredComponent == null) throw new Exception($"Component interface '{componentInterfaceFullname}' is not registered.");

                var parentExecutingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == parentComponent.TXID.xid);
                if (parentExecutingComponent == null) throw new Exception($"Exececuting component with id'{parentComponent.TXID.xid}' not found in execution thread with id'{parentComponent.TXID.tid}.");

                if (registeredComponent == null) throw new Exception($"Component interface '{componentInterfaceFullname}' is not registered.");
                var componentInterfaceName = componentInterfaceFullname.Split('.').LastOrDefault();

                int nextExecutingID = executionThread.ExecutingComponents.Count(); // Zero based.
                var nextTXID = new TXID(executionThread.ID, nextExecutingID).ToString();
                executingComponent = new ExecutingComponent()
                {
                    ExecutingID = nextExecutingID,
                    InterfaceFullname = componentInterfaceFullname,
                    TypeFullname = typeof(T).FullName,
                    URL = $"{registeredComponent.DomainName}/{registeredComponent.PrimaryAppRoute}/{nextTXID}",
                    Breadcrumb = $"{parentExecutingComponent.Breadcrumb}/{componentInterfaceName}({nextExecutingID})",
                    ClientRef = clientRef,
                    ParentExecutingID = parentExecutingComponent.ExecutingID,
                    State = null, 
                    Title = registeredComponent.Title
                };
                executionThread.ExecutingComponents.Add(executingComponent);
                executionThread.ComponentExecutingID = nextExecutingID;
                executionThread.ExecutingComponentTitle = registeredComponent.Title;
            }

            T proxy;
            IComponent component;
            if (childComponentWithClientRefNotInThread)
            {
                proxy = CoreFunctions.CreateProxy<T>(typeof(IComponent));
                component = proxy as IComponent;
                component.TXID = new TXID(executionThread.ID, executingComponent.ExecutingID);
            }
            else
            {
                var stateJson = JsonConvert.SerializeObject(executingComponent.State);
                proxy = CoreFunctions.CreateProxy<T>(stateJson, typeof(IComponent));
                component = proxy as IComponent;
            }

            return component;
        }

        public IComponent ExtractComponentFromExecutionThread(ExecutionThread executionThread, int xid)
        {
            Component component = new OutcomeComponent();

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == xid);

            Type componentType = null;
            try
            {
                componentType = CoreFunctions.GetType(executingComponent.TypeFullname);
            }
            catch (Exception)
            {
            }
            if (componentType != null)
                component = Activator.CreateInstance(componentType) as Component;

            component.executionThread = executionThread;
            component.TXID = new TXID(executionThread.ID, xid);
            component.ClientRef = executingComponent.ClientRef;
            component.State = executingComponent.State;

            return component;
        }


        public async Task<string> ExecuteLogicalUnitAsync<T>(string txid) where T : LogicalUnit, new()
        {
            var lu = await LoadComponentFromExecutionThreadAsync<T>(txid);

            if (lu.executionThread.PendingOutcomeJson != null)
            {
                var outcome = JsonConvert.DeserializeObject<Outcome>(lu.executionThread.PendingOutcomeJson);

                Type outcomeType = null;
                try
                {
                    outcomeType = CoreFunctions.GetType(outcome.TypeName);
                }
                catch (Exception)
                {
                }
                if (outcomeType != null)
                    outcome = (Outcome)JsonConvert.DeserializeObject(lu.executionThread.PendingOutcomeJson, outcomeType);

                outcome.SourceComponent = ExtractComponentFromExecutionThread(lu.executionThread, outcome.SourceExecutionID);

                lu.HandleOutcome(outcome);
                lu.executionThread.PendingOutcomeJson = null;
            }

            var nextComponent = await lu.GetNextComponentAsync();

            var executingLu = lu.executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ExecutingID == lu.TXID.xid
            );
            executingLu.State = lu.State;

            if (nextComponent == null) // lu Done?
            {
                // Determine parent launcher...

                var executingLauncher = lu.executionThread.ExecutingComponents.FirstOrDefault(
                    e => e.ExecutingID == executingLu.ParentExecutingID
                );

                var launcherTXID = new TXID(lu.executionThread.ID, executingLauncher.ExecutingID).ToString();
                var launcher = await LoadComponentFromExecutionThreadAsync<LuLauncher>(launcherTXID);

                return launcher.ReturnUrl;
            }


            var nextExecutingComponent = lu.executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ExecutingID == nextComponent.TXID.xid
            );
            nextExecutingComponent.State = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(nextComponent));
            //executingComponent.State = JsonConvert.DeserializeObject<Dictionary<string, object>>(CoreFunctions.ProxyAsJson(nextComponent));
            lu.executionThread = await AppExecutionApiClient.SaveExecutionThreadAsync(lu.executionThread);

            return nextExecutingComponent.URL;
        }

        public async Task<string> RaiseOutcomeAsync(ITXID sourceTXID, Outcome outcome)
        {
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(sourceTXID.tid);
            return await RaiseOutcomeAsync(executionThread, sourceTXID, outcome);
        }

        public async Task<string> RaiseOutcomeAsync(IComponent sourceComponent, Outcome outcome)
        {
            var executionThread = await AppExecutionApiClient.LoadExecutionThreadAsync(sourceComponent.TXID.tid);
            return await RaiseOutcomeAsync(executionThread, sourceComponent.TXID, outcome);
        }

        private async Task<string> RaiseOutcomeAsync(ExecutionThread executionThread, ITXID sourceTXID, Outcome outcome)
        {

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ExecutingID == sourceTXID.xid
            );

            var parentExecutingComponent = executionThread.ExecutingComponents.FirstOrDefault(
                e => e.ExecutingID == executingComponent.ParentExecutingID
            );

            executionThread.ComponentExecutingID = parentExecutingComponent.ExecutingID;
            executionThread.ExecutingComponentTitle = parentExecutingComponent.Title;
            executionThread.ExecutionStatus = (int)LogicalUnitStatusEnum.Started; // TODO: Strictly necessary here?

            outcome.SourceExecutionID = sourceTXID.xid;
            outcome.TargetExecutionID = executingComponent.ParentExecutingID;

            executionThread.PendingOutcomeJson = JsonConvert.SerializeObject(outcome);
            //executionThread.PendingOutcomeJson = CoreFunctions.ProxyAsJson(outcome);

            await AppExecutionApiClient.SaveExecutionThreadAsync(executionThread);

            return parentExecutingComponent.URL;
        }

        // TODO: Need whole new method to kaunch a secondary app from the current execution thread.
        public async Task<string> RedirectLaunchAppAsync(ITXID parentTXID, string componentInterfaceFullname) // Potentially more params to come inc. returnTaskId, forceContextSearch, etc
        {
            // Temporary !!
            return await RedirectLaunchAppAsync(componentInterfaceFullname, null);
        }


        //public T NewOutcome<T>() where T : class
        //{
        //    var proxy = CoreFunctions.CreateProxy<T>(typeof(Outcome));
        //    //var outcome = proxy as IOutcome;
        //    //outcome.Data = new List<IOutcomeData>();
        //    return proxy;
        //}
    }
}
