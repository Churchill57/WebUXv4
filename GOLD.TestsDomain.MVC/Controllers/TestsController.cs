using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GOLD.TestsDomain.MVC.Controllers
{
    public class TestsController : Controller
    {
        // GET: Tests
        public RedirectResult LuTest1Primary(string txid)
        {
            return Redirect("https://bbc.co.uk");
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