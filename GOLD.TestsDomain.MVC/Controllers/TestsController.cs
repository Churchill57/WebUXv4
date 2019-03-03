using GOLD.Core.Components;
using GOLD.Core.Interfaces;
using GOLD.Core.Models;
using GOLD.CustomerDomain.MVC.UserExperiences;
using GOLD.TestsDomain.MVC.LogicalUnits;
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
        public async  Task<ContentResult> LuTest1Primary(string txid)
        {
            //var luTest1 = Component.Load<LuTest1>(txid);
            var luTest1 = await Component.LoadAsync<LuTest1>(txid);
            //var luTest1 = await executionManager.LoadComponentFromExecutionThreadAsync<LuTest1>(new TXID(txid));

            var nextComponent = await luTest1.GetNextComponentAsync();

            luTest1.SomeInteger = 33;
            luTest1.SomeString = "1 2 3 testing...";
            luTest1.SomeCustomer = new EntityContext(3, "fred smith", "some lame description");

            //luTest1.Save();
            await luTest1.SaveAsync();

            //var uxA = await UseComponentAsync<UxA>("A"); // Only possible if concrete type UxA is known i.e. same domain!
            //var uxA = await UseComponentAsync<IUxA>("A"); // Use interface in different domain where concrete type is not available!
            var uxA = new UxA();

            //await executionManager.SaveComponentToExecutionThreadAsync(luTest1);

            return Content($"=>...txid={txid}");


        }

        public ContentResult TestsDomainEntryPointNot()
        {
            return Content("Welcome to the Tests Domain. Please use specific entry point actions for logical unit and user experiences.");
        }

        public ContentResult UxA(string txid)
        {
            return Content("Hello from UxA");
        }
        public ContentResult UxB(string txid)
        {
            return Content("Hello from UxB");
        }
    }
}