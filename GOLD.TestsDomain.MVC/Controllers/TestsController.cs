using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.TestsDomain.Interfaces;
using GOLD.TestsDomain.MVC.LogicalUnits;
using GOLD.TestsDomain.MVC.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GOLD.TestsDomain.MVC.UserExperiences;

namespace GOLD.TestsDomain.MVC.Controllers
{
    public class TestsController : Controller
    {
        private readonly IExecutionManager executionManager;

        public TestsController(IExecutionManager executionManager)
        {
            this.executionManager = executionManager;
        }

        // GET: TestsEE
        //public RedirectResult LuTest1Primary(string txid)
        //{
        //    return Redirect("https://bbc.co.uk");
        //}
        public async Task<RedirectResult> LuTest1Primary(string txid)
        {
            return Redirect(await executionManager.ExecuteLogicalUnitAsync<LuTest1>(txid));
        }

        public async Task<RedirectResult> LuTest1Done(string txid)
        {
            //return Redirect(await executionManager.RaiseOutcomeAsync(new TXID(txid), new ComponentDoneOutcome()));
            return Redirect(await Component.RedirectRaiseOutcomeAsync(txid, new ComponentDoneOutcome()));
        }

        public ContentResult TestsDomainEntryPointNot()
        {
            return Content("Welcome to the Tests Domain. Please use specific entry point actions for logical unit and user experiences.");
        }

        public async Task<ViewResult> UxA(string txid)
        {
            var UxA = await executionManager.LoadComponentInterfaceFromExecutionThreadAsync<Interfaces.IUxA>(txid);
            ViewBag.txid = txid;
            return View(UxA);
        }
        public async Task<ViewResult> UxB(string txid)
        {
            var UxB = await executionManager.LoadComponentInterfaceFromExecutionThreadAsync<Interfaces.IUxB>(txid);
            ViewBag.txid = txid;
            return View(UxB);
        }

        public async Task<RedirectResult> GoToUxA(string txid)
        {
            //var UxA = await executionManager.LoadComponentFromExecutionThreadAsync<UxA>(txid);
            var UxA = await executionManager.LoadComponentInterfaceFromExecutionThreadAsync<Interfaces.IUxA>(txid);
            UxA.SomeInterfacePropertyA = $"Outcome raised at {DateTime.Now}";

            var outcome = new GotoUxAOutcome();
            outcome.DevilsOwn = 666;
            outcome.Data = new Dictionary<string, object>() { { "alpha", 1 }, { "beta", 2 } };
            var nextUrl = await executionManager.RaiseOutcomeAsync((IComponent)UxA, outcome);

            return Redirect(nextUrl);


        }

        public async Task<RedirectResult> GoToUxB(string txid)
        {
            //var UxB = await executionManager.LoadComponentFromExecutionThreadAsync<UxB>(txid);
            var UxB = await executionManager.LoadComponentInterfaceFromExecutionThreadAsync<Interfaces.IUxB>(txid);
            UxB.SomeInterfacePropertyB = $"Outcome raised at {DateTime.Now}";

            var outcome = new GotoUxBOutcome();
            outcome.WhatTheHell = true;
            outcome.Data = new Dictionary<string, object>() { { "gamma", 3 }, { "delte", 4 } };
            outcome.List = new List<string>() { "a", "b", "c" };
            var nextUrl = await executionManager.RaiseOutcomeAsync((IComponent)UxB, outcome);

            return Redirect(nextUrl);


        }

    }
}