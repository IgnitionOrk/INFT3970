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
        // Uploads the screenshot to the server for the program structure application.
        // @param screenshot: a 64 base string containing important information of the screenshot.
        // @param to: Email provided by the user, that has been determine to be in the correct formatting. 
        [HttpPost]
        public ActionResult Upload(string screenshot, string to)
        {
            bool pSaved = false;
            try
            {
                // Extract the student number so it can then be implemented with the Screenshot jpeg file. 
                string studentNumber = to.Substring(0, to.IndexOf("@"));

                // upload the screenshot to the server, and issue the file path of the jpeg.
                string filePath = ProgramPlannerEmail.uploadTo(screenshot, studentNumber);

                // Email the program structure to the user, using the filePath to find the jpeg. 
                ProgramPlannerEmail.email(to, filePath);
                pSaved = true;
            }
            catch(Exception e)
            {
                pSaved = false;
            }
            return Json(new { saved = pSaved });
        }
    }
}
