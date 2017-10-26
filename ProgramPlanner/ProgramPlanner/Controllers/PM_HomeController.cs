using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPlanner.Controllers
{
    public class PM_HomeController : Controller
    {
        // GET: PM_Home
        public ActionResult Index()
        {
            return View();
        }
    }
}