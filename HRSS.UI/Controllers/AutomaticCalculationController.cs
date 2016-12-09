using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppMockup.Controllers
{
    public class AutomaticCalculationController : Controller
    {
        // GET: AutoCalculation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAutomaticCalculation()
        {
            return View();
        }
    }
}