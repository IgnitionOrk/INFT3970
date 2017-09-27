using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ProgramPlanner
{
    public static class ProgramPlannerEmail
    {
        private static string from = "universitystudent702@gmail.com";
        private static string password = "newcastleUniversity";
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"Screenshots\";
        private static SmtpClient smtp = new SmtpClient(){
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new System.Net.NetworkCredential(from, password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenshot"></param>
        public static string uploadTo(string screenshot, string studentNumber) {
            // String should be the path to the applications own image folder. 
            string filePath = folderPath + studentNumber+"-"+ DateTime.Today.Date.ToString("dd/MM/yyyy").Replace("/", "-").Replace(" ", "-").Replace(":", "") + ".jpg";
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

        public static void email(string to, string filePath) {
            Bitmap bMap = new Bitmap(filePath);
            MemoryStream stream = new MemoryStream();
            bMap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            stream.Position = 0;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(from);
            msg.To.Add(to);
            msg.Subject = "It worked";
            msg.Body = "Yeah Boi";
            msg.Subject = "IT WORK";
            msg.Attachments.Add(new Attachment(stream, "ProgramStructure.jpg"));

            smtp.Send(msg);
        }
    }
}