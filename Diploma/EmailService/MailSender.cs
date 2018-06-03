using Diploma.BLL;
using Diploma.Database;
using Diploma.Models;
using Diploma.Security;
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
        public static void BuildRegistrationEmailTemlplate(string userEmail)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/") + "Text" + ".cshtml");
                var user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                if (user != null)
                {
                    var url = "http://localhost:57412/" + "User/Confirm?userEmail=" + userEmail + "&hash=" + HashService.GenerateUserHash(user);
                    body = body.Replace("@ViewBag.ConfirmationLink", url);
                    body = body.ToString();
                    BuildEmailTemlplate("Twoje konto zostało utworzone", body, user.Email);
                }
            }
        }

        public static void BuildPasswordRecoveryEmailTemplate(string userEmail)
        {
            using (DiplomaDBContext db = new DiplomaDBContext())
            {
                string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/") + "Password" + ".cshtml");
                var user = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
                if (user != null)
                {
                    var url = "http://localhost:57412/" + "User/PasswordRecovery?userEmail=" + userEmail + "&hash=" + HashService.GenerateUserHash(user);
                    body = body.Replace("@ViewBag.ConfirmationLink", url);
                    body = body.ToString();
                    BuildEmailTemlplate("Przypomnienie hasła", body, user.Email);
                }
            }
        }

        public static void BuildTaskEmailToPromo(string userEmail, Guid taskId, Guid promoId, string messageToPromo = "")
        {
            using (var db = new DiplomaDBContext())
            {
                string body =
                  System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/") + "ToPromo" + ".cshtml");
                var user = db.ListOfUsers.FirstOrDefault(x => x.Email == userEmail);

                if (user != null)
                {
                    var task = db.ListOfProjectTasks.SingleOrDefault(x => x.Id == taskId);
                    body = body.Replace("$$Task.Title", task.Title);
                    body = body.Replace("$$Task.Description", task.Description);
                    body = body.Replace("$$Message", messageToPromo);
                    body = body.Replace("$$User.FirstName", user.FirstName);
                    body = body.Replace("$$User.LastName", user.LastName);

                    var title = user.FirstName + " " + user.LastName + " - Prośba o pomoc. Diploma";
                    var emailToPromo = db.ListOfPromos.SingleOrDefault(x => x.Id == promoId).Email;

                    BuildEmailTemlplate(title, body, emailToPromo);
                }
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
            client.Credentials = new System.Net.NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["Email"], System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]);
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