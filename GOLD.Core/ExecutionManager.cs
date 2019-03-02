using GOLD.AppExecution.ApiClient;
using GOLD.AppExecution.ApiModels;
using GOLD.AppRegister.ApiClient;
using GOLD.AppRegister.ApiModels;
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

        private static readonly Lazy<AppRegisterApiClient> lazyAppRegister = new Lazy<AppRegisterApiClient>(() => new AppRegisterApiClient(httpClientForAppRegister));
        private static AppRegisterApiClient AppRegister => lazyAppRegister.Value;


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

        private static readonly Lazy<AppExecutionApiClient> lazyAppExecution = new Lazy<AppExecutionApiClient>(() => new AppExecutionApiClient(httpClientForAppExecution));
        private static AppExecutionApiClient AppExecution => lazyAppExecution.Value;

        /// <summary>
        /// Returns URL of component instance after initiating a new execution thread.
        /// </summary>
        /// <param name="componentInterface"></param>
        /// <returns></returns>
        public async Task<string> RedirectLaunchAppAsync(string componentInterfaceFullname, string returnUrl) // Potentially more params to come inc. returnTaskId, forceContextSearch, etc
        {
            // Get info about the registered component.
            var registeredComponent = await AppRegister.GetComponentByInterfaceFullName(componentInterfaceFullname);
            // TODO: What if requested component interface is not registered?
            var componentInterfaceName = componentInterfaceFullname.Split('.').LastOrDefault();
            var componentUrl = $"{registeredComponent.DomainName}/{registeredComponent.PrimaryAppRoute}";

            var launcher = new LuLauncher() { ClientRef = componentInterfaceFullname,  ReturnUrl = returnUrl };

            var thread = new ExecutionThread()
            {
                ExecutingComponents = new List<ExecutingComponent>()
                {
                    new ExecutingComponent()
                    {
                        ExecutingID = 1, // Starts at 1 and will later be incremented for each additional component instance executing within the thread.
                        InterfaceFullname = typeof(LuLauncher).FullName, // a system Launcher Component is always the entry point to a thread
                        //URL = "?", // Relevant for LuLauncher??
                        Breadcrumb = "LuLauncher(1)", // Includes ExecutingID.
                        ParentExecutingID = 0, // No parent for entry point launcher
                        ClientRef = launcher.ClientRef,
                        State = launcher.State,
                        Title = registeredComponent.Title
                    },
                    new ExecutingComponent() // The root component (i.e. the component launched for execution).
                    {
                        ExecutingID = 2, // Starts at 1 and is incremented for each component executed within the thread.
                        InterfaceFullname = componentInterfaceFullname,
                        URL = componentUrl,
                        Breadcrumb = $"LuLauncher(1)/{componentInterfaceName}(2)",
                        ClientRef = "LuLauncher(1)", 
                        ParentExecutingID = 1, // LuLauncher acts as parent to root component.
                        State = null,
                        Title = registeredComponent.Title
                    }
                },
                ComponentExecutingID = 2, // Execution starts at the root component.
                ExecutingComponentTitle = registeredComponent.Title,
                ExecutionStatus = (int)LogicalUnitStatusEnum.Initialised,
                LaunchInputs = null,
                LockDateTime = DateTime.Now,
                LockUserID = Guid.NewGuid().ToString(), // TODO: Determine user ID
                LockUserName = "Anonymous",
                RootComponentTitle  = registeredComponent.Title //JH ,
                //JHPendingOutcome = null
            };

            var persistedThread = await AppExecution.SaveNewExecutionThreadAsync(thread);

            return await RedirectResumeExecutionThreadAsync(persistedThread.ID);
        }


        public async Task<string> RedirectResumeExecutionThreadAsync(int tid)
        {
            var executionThread = await AppExecution.LoadExecutionThreadAsync(tid);
            return await RedirectResumeExecutionThreadAsync(executionThread);
        }

        public async Task<string> RedirectResumeExecutionThreadAsync(ExecutionThread executionThread)
        {
            var componentExecuting = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == executionThread.ComponentExecutingID);
            componentExecuting.URL = $"{componentExecuting.URL}/{executionThread.ID}.{executionThread.ComponentExecutingID}/";
            executionThread.ExecutionStatus = (int)LogicalUnitStatusEnum.Started;
            executionThread = await AppExecution.SaveExecutionThreadAsync(executionThread);
            return componentExecuting.URL;
        }

        //public async Task<ExecutionThread> LoadExecutionThreadAsync(int tid)
        //{
        //    return await AppExecution.LoadExecutionThreadAsync(tid);
        //}

        public async Task<T> LoadComponentFromExecutionThreadAsync<T>(string txid) where T : Component, new()
        {
            return await LoadComponentFromExecutionThreadAsync<T>(new TXID(txid));
        }

        public async Task<T> LoadComponentFromExecutionThreadAsync<T>(TXID txid) where T : Component, new()
        {
            var executionThread = await AppExecution.LoadExecutionThreadAsync(txid.tid);
            return LoadComponentFromExecutionThread<T>(executionThread, txid.xid);
        }

        private T LoadComponentFromExecutionThread<T>(ExecutionThread executionThread, int xid) where T : Component, new()
        {
            var t = new T();
            var component = t as LogicalUnit;

            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == xid);

            //Component.executionManager = this; // See constructor
            component.TXID = new TXID(executionThread.ID, xid);
            component.ClientRef = executingComponent.ClientRef;
            component.State = executingComponent.State;

            return t;
        }

        public async Task SaveComponentToExecutionThreadAsync(Component component)
        {
            var executionThread = await AppExecution.LoadExecutionThreadAsync(component.TXID.tid);
            await SaveComponentToExecutionThreadAsync(component, executionThread);
        }

        private async Task SaveComponentToExecutionThreadAsync(Component component, ExecutionThread executionThread)
        {
            var executingComponent = executionThread.ExecutingComponents.FirstOrDefault(e => e.ExecutingID == component.TXID.xid);
            executingComponent.State = component.State;
            executionThread = await AppExecution.SaveExecutionThreadAsync(executionThread);
        }

    }
}
