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

        //public async Task<ActionResult> Index()
        //{
        //    Domain domain = new Domain();

        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var domainID = "2476C153-D94C-40B6-B0C3-221002AA5379";
        //    var response = await client.GetAsync($"api/Domains/{domainID}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var EmpResponse = response.Content.ReadAsStringAsync().Result;
        //        domain = JsonConvert.DeserializeObject<Domain>(EmpResponse);
        //    }

        //    var domains = new List<Domain>();
        //    domains.Add(domain);
        //    return View(domains);
        //}
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
        }

    }
}