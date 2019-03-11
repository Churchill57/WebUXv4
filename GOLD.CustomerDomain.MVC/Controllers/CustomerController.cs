using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.CustomerDomain.ApiClient;
using GOLD.CustomerDomain.ApiClient.Interfaces;
using GOLD.CustomerDomain.ApiModels;
using GOLD.CustomerDomain.MVC.LogicalUnits;
using GOLD.CustomerDomain.MVC.ViewModels;
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
using GOLD.Core.Components;

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


        public async Task<ActionResult> UxCustomerSearchCriteria(string txid)
        {
            var uxCustomerSearchCriteria = await executionManager.LoadComponentFromExecutionThreadAsync<UxCustomerSearchCriteria>(txid);
            return View(uxCustomerSearchCriteria.GetViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> UxCustomerSearchCriteria(string txid, [Bind(Include = "Name, DOB", Prefix = "Criteria")] CustomerSearchCriteriaViewModel customerSearchCriteria) // Get CaptureSearchCriteria
        {
            if (!ModelState.IsValid) return View(customerSearchCriteria);
            var uxCustomerSearchCriteria = await executionManager.LoadComponentFromExecutionThreadAsync<UxCustomerSearchCriteria>(txid);
            await uxCustomerSearchCriteria.SaveCustomerSearchCriteriaAsync(customerSearchCriteria);
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
            return Redirect(await uxCustomerSearchCriteria.RedirectRaiseOutcomeAsync(new ComponentDoneOutcome()));
        }

        public async Task<ActionResult> UxSelectCustomer(string txid)
        {
            var uxSelectCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxSelectCustomer>(txid);
            return View(await uxSelectCustomer.GetSearchResultsViewModelAsync());
        }

        public async Task<ActionResult> UxSelectCustomer_CustomerSelected(string txid, int customerId, string fullName)
        {
            var uxSelectCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxSelectCustomer>(txid);
            await uxSelectCustomer.SaveSelectedCustomerContextAsync(customerId, fullName);
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
            return Redirect(await uxSelectCustomer.RedirectRaiseOutcomeAsync(new ComponentDoneOutcome()));
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
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentBackOutcome()));
            return Redirect(await Component.RedirectRaiseOutcomeAsync(txid, new ComponentBackOutcome()));
        }



        public async Task<ActionResult> UxPreviewCustomer(string txid)
        {
            var uxPreviewCustomer = await executionManager.LoadComponentFromExecutionThreadAsync<UxPreviewCustomer>(txid);
            return View(await uxPreviewCustomer.GetViewModel());
        }

        public async Task<ActionResult> UxPreviewCustomer_Done(string txid)
        {
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
            return Redirect(await Component.RedirectRaiseOutcomeAsync(txid, new ComponentDoneOutcome()));
        }

    }
}
