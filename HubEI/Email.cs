using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HubEI
{
    public class Email
    {
        public static void SendEmail(string strToEmail, string strSubject, string strBody)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("hubei.gp@gmail.com", "f00n!l06");

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("hubei.gp@gmail.com")
            };

            mailMessage.To.Add(strToEmail);
            mailMessage.Body = strBody;
            mailMessage.Subject = strSubject;
            mailMessage.IsBodyHtml = true;
            smtpClient.Send(mailMessage);
        }
    }
}
