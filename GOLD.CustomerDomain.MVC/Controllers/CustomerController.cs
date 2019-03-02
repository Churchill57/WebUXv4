using GOLD.Core.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.MVC.LogicalUnits;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOLD.CustomerDomain.MVC.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController(IExecutionManager executionManager)
        {
            this.executionManager = executionManager;
        }






        static string Baseurl = "http://localhost:19803/";
        private static HttpClient client = new HttpClient() { BaseAddress = new Uri(Baseurl) };
        private readonly IExecutionManager executionManager;

        public async Task<ActionResult> Index()
        {
            var lu = new LuPreviewCustomer();


            List<Customer> customers = new List<Customer>();

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("api/customer");

            if (response.IsSuccessStatusCode)
            {
                var EmpResponse = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customer>>(EmpResponse);
            }
            return View(customers);
        }

        //// GET: Customer/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Customer/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Customer/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Customer/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Customer/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Customer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Customer/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
