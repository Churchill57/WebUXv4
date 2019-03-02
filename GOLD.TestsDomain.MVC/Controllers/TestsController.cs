﻿using GOLD.Core.AppManagement;
using GOLD.Core.AppManagement.Interfaces;
using GOLD.Core.Models;
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

        // GET: Tests
        //public RedirectResult LuTest1Primary(string txid)
        //{
        //    return Redirect("https://bbc.co.uk");
        //}
        public async Task<ContentResult> LuTest1Primary(string txid)
        {
            var luTest1 = await executionManager.LoadComponentFromExecutionThreadAsync<LuTest1>(new TXID(txid));

            luTest1.SomeInteger = 99;
            luTest1.SomeString = "1 2 3 testing...";
            luTest1.SomeCustomer = new EntityContext(1, "fred smith", "some lame description");

            await executionManager.SaveComponentToExecutionThreadAsync(luTest1);

            return Content($"=>...txid={txid}");


        }

        public ContentResult LuTest1()
        {
            return Content("Use LuTest1Primary for entry point to logical unit LuTest1");
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