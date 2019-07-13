using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.Common.Helpers
{
    public class MailHelper
    {

        public static bool SendMail(string body, string to, string subject, bool isHtml= true)
        {
            return SendMail(body, new List<string> { to }, subject, isHtml);
        }
 
        public static bool SendMail(string body, List<string> to, string subject, bool isHtml= true)
        {
            bool result = false;

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("saliihdonmezz@gmail.com", "salih1903");

                var message = new MailMessage();
                message.From = new MailAddress(ConfigHelper.Get<string>("MailUser"));

                to.ForEach(x =>
                {
                    message.To.Add(new MailAddress(x));
                });

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (SmtpClient smtp = new SmtpClient(
                    ConfigHelper.Get<string>("MailHost"),
                    ConfigHelper.Get<int>("MailPort")))

                {
                    smtp.EnableSsl = true;
                    smtp.Credentials =
                        new NetworkCredential(
                        ConfigHelper.Get<string>("MailUser"),
                        ConfigHelper.Get<string>("MailPass"));

                    smtp.Send(message);
                    result = true;
                }
            }

            catch (Exception)
            {

            }
            return result;
        }
    }
}