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
        private static string from = "hello@myuniplanner.com";
        private static string password = "L0v3uni!";
        private static string format = ".jpg";

        // Generic SMTP client. 
        private static SmtpClient smtp = new SmtpClient()
        {
            Host = "smtp.zoho.com",
            Port = 587,
            Credentials = new System.Net.NetworkCredential(from, password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        // Uploads the screenshot to the server for the program structure application.
        // @param screenshot: a 64 base string containing important information of the screenshot.
        // @param to: Email provided by the user, that has been determine to be in the correct formatting. 
        [HttpPost]
        public ActionResult Upload(string screenshot, string name, string to)
        {
            bool pSaved = false;
            try
            {
                Bitmap bMap = null;
                byte[] decoded64base = Convert.FromBase64String(screenshot);
                using (var ms = new MemoryStream(decoded64base)) {
                    bMap = new Bitmap(ms);
                }
                
                // Email the program structure to the user, using the filePath to find the jpeg. 
                email(to, name, bMap);
                pSaved = true;
            }
            catch (Exception e)
            {
                // Unsuccessful completion of function
                pSaved = false;
            }
            return Json(new { saved = pSaved });
        }
        /// <summary>
        /// Emails the screenshot of the program structure to the email provided by the user 'to'.
        /// </summary>
        /// <param name="to">Email provided by the user</param>
        /// <param name="filePath">Location of the screenshot. Must have been issued prior.</param>
        private static void email(string to, string name, Bitmap bMap)
        {
            MemoryStream stream = new MemoryStream();
            bMap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(to);
            msg.CC.Add(from);
            msg.Subject = "Degree Structure";
            msg.IsBodyHtml = true;
            AlternateView bodyView = AlternateView.CreateAlternateViewFromString(htmlBody(name), null, "text/html");
            msg.AlternateViews.Add(bodyView);
            msg.Attachments.Add(new Attachment(stream, "ProgramStructure.jpg"));
            smtp.Send(msg);
        }
        /// <summary>
        /// Returns a standard html email body.
        /// </summary>
        /// <returns></returns>
        private static string htmlBody(string name)
        {
            string body = "<html>";
            body += "<head><title>Degree Structure</title></head>";
            body += "<body>";
            body += "<p>Hello <b>" + name + "</b>,<br>";
            body += "Thank you for using My Uni Planner. Please see the attachment for your university plan<br>";
            body += "Kind Regards,<br>Your beloved team at My Uni Planner</p>";
            body += "</body>";
            body += "</html>";
            return body;
        }
    }
}
