using GOLD.Core.AppManagement.Interfaces;
using GOLD.CustomerDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult RunApp()
        {
            var url = executionManager.StartExecutionThread(typeof(ILuPreviewCustomer));

            return View();
        }
    }
}