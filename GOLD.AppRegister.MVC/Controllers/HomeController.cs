using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GOLD.AppRegister.ApiModels;
using Newtonsoft.Json;

namespace GOLD.AppRegister.MVC.Controllers
{
    public class HomeController : Controller
    {
        static string Baseurl = "http://localhost:27169/";
        private static HttpClient client = new HttpClient() { BaseAddress = new Uri(Baseurl) };

        public async Task<ActionResult> Index()
        {
            var component = new ComponentByInterfaceFullName();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var componentInterfaceFullName = "GOLD.CustomerDomain.MVC.LogicalUnits.ILuPreviewCustomer";
            // IMPORTANT: Trailing slash in URL mitigatse period problem in interface full name (e.g. namespace)!
            var response = await client.GetAsync($"api/Components/GetComponentByInterfaceFullName/{componentInterfaceFullName}/");

            if (response.IsSuccessStatusCode)
            {
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                component = JsonConvert.DeserializeObject<ComponentByInterfaceFullName>(EmpResponse);
            }

            return View("DetailsForComponent", component);

            //var response = await httpClient.GetAsync($"api/ExecutionThreads/{ID}");
            //response.EnsureSuccessStatusCode();
            //return response.Content.ReadAsAsync<ExecutionThread>().Result;

        }

    }
}