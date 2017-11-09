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
        private static string from = "universitystudent702@gmail.com";
        private static string password = "newcastleUniversity";
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"Screenshots\";
        private static string format = ".jpg";

        // Generic SMTP client. 
        private static SmtpClient smtp = new SmtpClient()
        {
            Host = "smtp.gmail.com",
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
                // upload the screenshot to the server, and issue the file path of the jpeg.
                string filePath = uploadTo(screenshot, name);

                // Email the program structure to the user, using the filePath to find the jpeg. 
                email(to, name, filePath);
            }
            catch (Exception e)
            {
                // Unsuccessful completion of function
                pSaved = false;
            }
            return Json(new { saved = pSaved });
        }

        /// <summary>
        /// Uploads the screenshot of the program structure to the server, with the student number
        /// in the file name. This is all program structures are unique. 
        /// </summary>
        /// <param name="screenshot"> The 64 base string of the program structure</param>
        /// <param name="studentNumber">Unique student number</param>
        /// <returns>The file path of the location of the newly uploaded screenshot</returns>
        private static string uploadTo(string screenshot, string name)
        {
            // String should be the path to the applications own image folder. 
            string filePath = folderPath;
            filePath += name;
            filePath += "-Date-" + DateTime.Today.ToString("dd-MM-yyyy").Replace(" ", "-");
            filePath += "-Time-" + DateTime.Now.ToString("hh:mm:ss").Replace(":", "-");
            filePath += format;
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(screenshot);
                    bw.Write(data);
                    bw.Close();
                }
            }
            return filePath;
        }
        /// <summary>
        /// Emails the screenshot of the program structure to the email provided by the user 'to'.
        /// </summary>
        /// <param name="to">Email provided by the user</param>
        /// <param name="filePath">Location of the screenshot. Must have been issued prior.</param>
        private static void email(string to, string name, string filePath)
        {
            Bitmap bMap = new Bitmap(filePath);
            MemoryStream stream = new MemoryStream();
            bMap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(to);
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
            body += "See the attachment for you university plan<br>";
            body += "Kind Regards<br>My Uni Planner</p>";
            body += "</body>";
            body += "</html>";
            return body;
        }
    }
}
