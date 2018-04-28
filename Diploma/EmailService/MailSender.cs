using Diploma.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Diploma.EmailService
{
    public static class MailSender
    {
        public static void BuildEmailTemlplate(string userEmail, Guid activationId)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/") + "Text" + ".cshtml");
                var regInfo = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                var url = "http://localhost:57412/" + "Home/Confirm?userEmail=" + userEmail + "?activationId" + activationId;
                body = body.Replace("@ViewBag.ConfirmationLink", url);
                body = body.ToString();
                BuildEmailTemlplate("Twoje konto zostało utworzone", body, regInfo.Email);
            }
        }

        public static void BuildEmailTemlplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "diploma.register@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }

            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);
        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("diploma.register@gmail.com", "ctor{dip.reg123}");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}