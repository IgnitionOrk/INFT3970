using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
namespace ProgramPlanner.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email/Create
        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string screenshot, string to)
        {
            bool pSaved = false;
            try
            {
                // Extract the student number so it can then be implemented with the Screenshot jpeg file. 
                string studentNumber = to.Substring(0, to.IndexOf("@"));
                string filePath = ProgramPlannerEmail.uploadTo(screenshot, studentNumber);
                ProgramPlannerEmail.email(to, filePath);
                pSaved = true;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                pSaved = false;
            }
            return Json(new { saved = pSaved });
        }
    }
}
