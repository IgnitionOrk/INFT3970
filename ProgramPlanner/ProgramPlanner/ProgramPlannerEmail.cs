using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
/*
    Author: Ryan Cunneen
    Date created: 27-Sep-2017
    Date Modified: 28-Sep-2017.
*/
namespace ProgramPlanner
{
    public static class ProgramPlannerEmail
    {
        private static string from = "universitystudent702@gmail.com";
        private static string password = "newcastleUniversity";
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"Screenshots\";
        private static string format =  ".jpg";
        private static SmtpClient smtp = new SmtpClient(){
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new System.Net.NetworkCredential(from, password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };
        /// <summary>
        /// Uploads the screenshot of the program structure to the server, with the student number
        /// in the file name. This is all program structures are unique. 
        /// </summary>
        /// <param name="screenshot"> The 64 base string of the program structure</param>
        /// <param name="studentNumber">Unique student number</param>
        /// <returns>The file path of the location of the newly uploaded screenshot</returns>
        public static string uploadTo(string screenshot, string studentNumber) {
            // String should be the path to the applications own image folder. 
            string filePath = folderPath;
            filePath += studentNumber;
            filePath += "-Date-" + DateTime.Today.ToString("dd-MM-yyyy").Replace(" ", "-");
            filePath += "-Time-"+DateTime.Now.ToString("hh:mm:ss").Replace(":", "-");
            filePath += format;
            Debug.WriteLine("filePath: "+filePath);
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
        public static void email(string to, string name, string filePath) {
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
            body += "<p>Hello <b>"+name+"</b>,<br>";
            body += "See the attachment for you university plan<br>";
            body += "Kind Regards<br>My Uni Planner</p>";
            body += "</body>";
            body += "</html>";
            return body;
         }
    }
}