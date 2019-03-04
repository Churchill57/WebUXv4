using GOLD.Core.Interfaces;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GOLD.Portal.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExecutionManager executionManager;

        public HomeController(IExecutionManager executionManager)
        {
            this.executionManager = executionManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> LuTest1()
        {
            var url = Request?.UrlReferrer?.AbsoluteUri ?? "";
            var nextUrl = await executionManager.RedirectLaunchAppAsync("GOLD.TestsDomain.Interfaces.ILuTest1", url);
            return Redirect(nextUrl);
        }
    }
}