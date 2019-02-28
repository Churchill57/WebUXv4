using GOLD.AppExecution.ApiClient;
using GOLD.AppExecution.ApiModels;
using GOLD.AppRegister.ApiClient;
using GOLD.AppRegister.ApiModels;
using GOLD.Core.AppManagement.Interfaces;
using GOLD.Core.Components;
using GOLD.Core.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement
{
    public class ExecutionManager : IExecutionManager
    {
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
        public async Task<string> RedirectLaunchApp(string componentInterfaceFullname)
        {
            // Get info about the registered component.
            var registeredComponent = await AppRegister.GetComponentByInterfaceFullName(componentInterfaceFullname);
            var componentInterfaceName = componentInterfaceFullname.Split('.').LastOrDefault();
            var componentUrl = $"{registeredComponent.DomainName}/{registeredComponent.PrimaryAppRoute}";

            var thread = new ExecutionThread()
            {
                ExecutingComponents = new List<ExecutingComponent>()
                {
                    new ExecutingComponent() // System Launcher Component is entry point to thread
                    {
                        ExecutingID = 1, // Starts at 1 and is incremented for each component executed within the thread.
                        InterfaceFullname = typeof(LuLauncher).FullName,
                        URL = "?", // Relevant for LuLauncher??
                        Breadcrumb = "LuLauncher(1)", // Includes ExecutingID.
                        ParentExecutingID = 0, // No parent
                        ClientRef = componentInterfaceName, // The name of what will be the root component.
                        State = null,
                        Title = registeredComponent.Title
                    },
                    new ExecutingComponent() // The root component (i.e. the component launched for execution).
                    {
                        ExecutingID = 2, // Starts at 1 and is incremented for each component executed within the thread.
                        InterfaceFullname = componentInterfaceFullname,
                        URL = componentUrl,
                        Breadcrumb = $"LuLauncher(1)/{componentInterfaceName}(2)",
                        ClientRef = componentInterfaceName, 
                        ParentExecutingID = 1, // LuLauncher acts as parent to root component.
                        State = null,
                        Title = registeredComponent.Title
                    }
                },
                ComponentExecutingID = 2, // Execution starts at the root component.
                ExecutingComponentTitle = registeredComponent.Title,
                ExecutionStatus = LogicalUnitStatusEnum.Initialised,
                LaunchInputs = null,
                LockDateTime = DateTime.Now,
                LockUserID = Guid.NewGuid().ToString(), // TODO: Determine user ID
                LockUserName = "Anonymous",
                RootComponentTitle  = registeredComponent.Title,
                PendingOutcome = null
            };

            var result = await AppExecution.SaveNewExecutionThread(thread);

           // PersistNewExecutionThread(thread);

            return $"{componentUrl}/123.2/";


            //throw new NotImplementedException();
        }


        //static string Baseurl = "http://localhost:27169/";
        //private static HttpClient client = new HttpClient() { BaseAddress = new Uri(Baseurl) };

        //public async Task<ActionResult> Index()
        //{
        //    List<Domain> domains = new List<Domain>();

        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await client.GetAsync("api/Domains");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var EmpResponse = response.Content.ReadAsStringAsync().Result;
        //        domains = JsonConvert.DeserializeObject<List<Domain>>(EmpResponse);
        //    }
        //    return View(domains);
        //}


    }
}
