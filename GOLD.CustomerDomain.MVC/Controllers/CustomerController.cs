using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.MVC.LogicalUnits;
using GOLD.CustomerDomain.MVC.Models;
using GOLD.CustomerDomain.MVC.UserExperiences;
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
        private readonly IExecutionManager executionManager;
        public CustomerController(IExecutionManager executionManager)
        {
            this.executionManager = executionManager;
        }

        public ContentResult CustomerDomainEntryPointNot()
        {
            return Content("Welcome to the Customer Domain. Please use specific entry point actions for logical unit and user experiences.");
        }


        public async Task<ActionResult> LuPreviewCustomerIncSearchAndSelect(string txid)
        {
            return Redirect(await executionManager.ExecuteLogicalUnitAsync<LuPreviewCustomerIncSearchAndSelect>(txid));
        }


        public async Task<ActionResult> UxCustomerSearchCriteria(string txid) // Get CaptureSearchCriteria
        {
            var uxCustomerSearchCriteria = await executionManager.LoadComponentFromExecutionThreadAsync<UxCustomerSearchCriteria>(txid);

            var model = new CustomerSearchCriteria
            {
                Name = uxCustomerSearchCriteria.CustomerName,
                DOB = uxCustomerSearchCriteria.CustomerDateOfBirth
            };

            ViewBag.ShowSwitchToAdvanced = uxCustomerSearchCriteria.ShowSwitchToAdvanced;
            ViewBag.ShowBackButton = uxCustomerSearchCriteria.ShowBackButton;
            ViewBag.BackButtonText = uxCustomerSearchCriteria.BackButtonText;
            ViewBag.SearchButtonText = uxCustomerSearchCriteria.SearchButtonText;

            ViewBag.txid = txid;
            return View(model);

        }

        [HttpPost]
        public async Task<ActionResult> UxCustomerSearchCriteria(string txid, [Bind(Include = "Name,DOB")] CustomerSearchCriteria customerSearchCriteria) // Get CaptureSearchCriteria
        {
            if (!ModelState.IsValid) return View(customerSearchCriteria);

            var uxCustomerSearchCriteria = await executionManager.LoadComponentFromExecutionThreadAsync<UxCustomerSearchCriteria>(txid);
            uxCustomerSearchCriteria.CustomerName = customerSearchCriteria.Name;
            uxCustomerSearchCriteria.CustomerDateOfBirth = customerSearchCriteria.DOB;
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

        public async Task<ActionResult> UxPreviewCustomer(string txid)
        {
            var uxPreviewCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxPreviewCustomer>(txid);
            ViewBag.txid = txid;
            return View(uxPreviewCustomer);

        }

        public async Task<ActionResult> UxSelectCustomer(string txid)
        {
            var TXID = new TXID(txid);
            var uxSelectCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxSelectCustomer>(txid);
            var xidSearch = uxSelectCustomer.SearchTaskId.Value;
            var txidSearch = new TXID(TXID.tid, xidSearch).ToString();
            var uxPerformCustomerSearch = await executionManager.LoadComponentAsync(txidSearch) as IUxPerformSearch<Customer>;

            var model = uxPerformCustomerSearch.PerformSearch().ToList();

            ViewBag.ShowBackButton = uxSelectCustomer.ShowBackButton;
            ViewBag.BackButtonAsLink = uxSelectCustomer.BackButtonAsLink;
            ViewBag.BackButtonText = uxSelectCustomer.BackButtonText;
            ViewBag.SelectButtonText = uxSelectCustomer.SelectButtonText;
            ViewBag.ShowPreviewLink = uxSelectCustomer.ShowPreviewLink;
            ViewBag.txid = txid;
            return View(model);

        }

        public async Task<RedirectResult> Back(string txid)
        {
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentBackOutcome()));
        }




        public async Task<RedirectResult> LuTest1Done(string txid)
        {
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

        //public async Task<ActionResult> ux1(string txid)
        //{

        //}

        //public async Task<ActionResult> u2x(string txid)
        //{

        //}

        //public async Task<ActionResult> ux3(string txid)
        //{

        //}




        //static string Baseurl = "http://localhost:19803/";
        //private static HttpClient client = new HttpClient() { BaseAddress = new Uri(Baseurl) };
        //private readonly IExecutionManager executionManager;

        //public async Task<ActionResult> Index()
        //{
        //    var lu = new LuPreviewCustomer();


        //    List<Customer> customers = new List<Customer>();

        //    client.DefaultRequestHeaders.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var response = await client.GetAsync("api/customer");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var EmpResponse = response.Content.ReadAsStringAsync().Result;
        //        customers = JsonConvert.DeserializeObject<List<Customer>>(EmpResponse);
        //    }
        //    return View(customers);
        //}

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
