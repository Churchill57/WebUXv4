using GOLD.Core.AppManagement.Interfaces;
using GOLD.Core.Components;
using GOLD.Core.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement
{
    public class ExecutionManager : IExecutionManager
    {
        //private const string GOLD_DomainID = "GOLD.DomainID";

        //private static string DomainID => ConfigurationManager.AppSettings[GOLD_DomainID];

        /// <summary>
        /// Returns URL of component instance after initiating a new execution thread.
        /// </summary>
        /// <param name="componentInterface"></param>
        /// <returns></returns>
        public string StartExecutionThread(Type componentInterface)
        {
            var thread = new ExecutionThread()
            {
                ExecutingComponents = new List<ExecutingComponent>()
                {
                    new ExecutingComponent() // System Launcher Component is entry point to thread
                    {
                        Breadcrumb = "LuLauncher(1)", // Includes ExecutingID.
                        ClientRef = componentInterface.Name, // The name of what will be the root component.
                        ComponentID = 0, // LuLauncher is a system LogicalUnit and is not registered.
                        ComponentName = typeof(LuLauncher).FullName,
                        ExecutingID = 1, // Starts at 1 and is incremented for each component executed within the thread.
                        ParentComponentID = 0, // No parent component for the LuLauncher entry point of the thread.
                        ParentExecutingID = 0,
                        State = null,
                        Title = "Get from registered component" // TODO: To be determined
                    },
                    new ExecutingComponent() // The root component (i.e. the component launched for execution).
                    {
                        Breadcrumb = $"LuLauncher/{componentInterface.Name}(2)",
                        ClientRef = componentInterface.Name, 
                        ComponentID = 0, // TODO: To be determined
                        ComponentName = typeof(LuLauncher).FullName, // Will be looked-up in App Registry
                        ExecutingID = 2, // Starts at 1 and is incremented for each component executed within the thread.
                        ParentComponentID = 0, // No parent component for the root component of the thread.
                        ParentExecutingID = 1, // LuLauncher acts as parent to root component.
                        State = null,
                        Title = "Get from registered component" // TODO: To be determined
                    }
                },
                ComponentExecutingID = 2, // Execution starts at the root component.
                ExecutingComponentTitle = "Get from registered component", // TODO: To be determined
                ExecutionStatus = LogicalUnitStatusEnum.Initialised,
                LaunchInputs = null,
                LockDateTime = DateTime.Now,
                LockUserID = Guid.NewGuid().ToString(),
                LockUserName = "Anonymous",
                RootComponentTitle  = "Get from registered component", // TODO: To be determined
                PendingOutcome = null
            };


            return null;
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
