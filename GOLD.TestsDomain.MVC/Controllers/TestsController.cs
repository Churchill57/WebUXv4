using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using GOLD.TestsDomain.MVC.LogicalUnits;
using GOLD.TestsDomain.MVC.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            var nextUrl = await executionManager.ExecuteLogicalUnitAsync<LuTest1>(txid);
            return Redirect(nextUrl);

            ////var luTest1 = Component.Load<LuTest1>(txid);
            //var luTest1 = await Component.LoadAsync<LuTest1>(txid);
            ////var luTest1 = await executionManager.LoadComponentFromExecutionThreadAsync<LuTest1>(new TXID(txid));

            //var nextComponent = await luTest1.GetNextComponentAsync();

            //luTest1.SomeInteger = 33;
            //luTest1.SomeString = "1 2 3 testing...";
            //luTest1.SomeCustomer = new EntityContext(3, "fred smith", "some lame description");

            ////luTest1.Save();
            //await luTest1.SaveAsync();
            ////await executionManager.SaveComponentToExecutionThreadAsync(luTest1);

            //return Content($"=>...txid={txid}");

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

        public async Task<RedirectResult> GoToUxB(string txid)
        {
            var UxB = await executionManager.LoadComponentInterfaceFromExecutionThreadAsync<Interfaces.IUxB>(txid);
            UxB.SomeInterfaceProperty = $"Outcome raised at {DateTime.Now}";

            var gotoUxBOutcome = new GotoUxBOutcome();
            gotoUxBOutcome.Data.Add("param1", "value1");
            gotoUxBOutcome.Data.Add("customer context", new EntityContext(99,"John Smith","A test customer context"));
            var nextUrl = await executionManager.RaiseOutcomeAsync((IComponent)UxB, gotoUxBOutcome);

            return Redirect(nextUrl);


        }

    }
}