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
            List<Domain> domains = new List<Domain>();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("api/Domains");

            if (response.IsSuccessStatusCode)
            {
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                domains = JsonConvert.DeserializeObject<List<Domain>>(EmpResponse);
            }
            return View(domains);
        }
    }
}