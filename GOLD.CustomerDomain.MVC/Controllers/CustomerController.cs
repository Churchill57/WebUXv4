using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.ApiClient;
using GOLD.CustomerDomain.ApiClient.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.MVC.LogicalUnits;
using GOLD.CustomerDomain.MVC.Models;
using GOLD.CustomerDomain.MVC.UserExperiences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private readonly ICustomerDomainApiClient customerDomainApiClient;

        public CustomerController(IExecutionManager executionManager, ICustomerDomainApiClient customerDomainApiClient)
        {
            this.executionManager = executionManager;
            this.customerDomainApiClient = customerDomainApiClient;
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
            await uxCustomerSearchCriteria.SaveAsync();
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

        public async Task<ActionResult> UxSelectCustomer(string txid)
        {
            var TXID = new TXID(txid);
            var uxSelectCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxSelectCustomer>(txid);
            var xidSearch = uxSelectCustomer.SearchTaskId.Value;
            var txidSearch = new TXID(TXID.tid, xidSearch).ToString();
            var uxPerformCustomerSearch = await executionManager.LoadComponentAsync(txidSearch) as IUxPerformSearch<Customer>;

            var model = await uxPerformCustomerSearch.PerformSearchAsync();

            ViewBag.ShowBackButton = uxSelectCustomer.ShowBackButton;
            ViewBag.BackButtonAsLink = uxSelectCustomer.BackButtonAsLink;
            ViewBag.BackButtonText = uxSelectCustomer.BackButtonText;
            ViewBag.SelectButtonText = uxSelectCustomer.SelectButtonText;
            ViewBag.ShowPreviewLink = uxSelectCustomer.ShowPreviewLink;
            ViewBag.txid = txid;
            return View(model);

        }

        public async Task<ActionResult> UxSelectCustomer_CustomerSelected(string txid, int customerId, string fullName)
        {
            var TXID = new TXID(txid);
            var uxSelectCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxSelectCustomer>(txid);

            uxSelectCustomer.SetCustomer(customerId, fullName);
            uxSelectCustomer.Save(); // TODO: Is explicit saver really necessary. Could not executionManager.RaiseOutcomeAsync somehow do the save if the Ux and/or execution thread is dirty?

            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

        public async Task<ActionResult> UxSelectCustomer_PreviewCustomer(string txid, int customerId)
        {
            var TXID = new TXID(txid);
            return Redirect(await executionManager.RedirectLaunchAppAsync(txid, "GOLD.SomeDomain.Interfaces.ISomeComponent"));
            // TODO: Properly launch 2ndary app
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

        public async Task<RedirectResult> Back(string txid)
        {
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentBackOutcome()));
        }



        public async Task<ActionResult> UxPreviewCustomer(string txid)
        {
            var uxPreviewCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxPreviewCustomer>(txid);

            var customerId = uxPreviewCustomer.CustomerContext.Id;
            var model = await uxPreviewCustomer.LoadCustomerAsync(customerId);

            ViewBag.PreviewDifferentCustomer = uxPreviewCustomer.PreviewDifferentCustomer;
            ViewBag.ShowBackButton = uxPreviewCustomer.ShowBackButton;
            ViewBag.BackButtonText = uxPreviewCustomer.BackButtonText;
            ViewBag.BackButtonAsLink = uxPreviewCustomer.BackButtonAsLink;
            ViewBag.DoneButtonText = uxPreviewCustomer.DoneButtonText;

            ViewBag.txid = txid;
            return View(model);
        }

        public async Task<ActionResult> UxPreviewCustomer_Done(string txid)
        {
            return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
        }

    }
}
