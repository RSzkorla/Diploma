using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {
        private DiplomaDBContext db = new DiplomaDBContext();

        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Terms()
        {
            ViewBag.Message = "To będzie super strona z regulaminem";

            return View();
        }

        public ActionResult FrequentlyAQ()
        {
            ViewBag.Message = "To będzie super strona z FAQ";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User newUser)
        {
            string registrationMessage = null;

            if (ModelState.IsValid)
            {
                try
                {
                    db.ListOfUsers.Add(newUser);
                    db.SaveChanges();
                    registrationMessage = "Na podany adres e-mail została wysłana wiadomość weryfikacjyjna.";
                    BuildEmailTemlplate(newUser.Email, newUser.ActivationId);
                    return RedirectToAction("Index", new { message = registrationMessage });
                }
                catch (Exception)
                {
                    registrationMessage = "Istnieje już konto o podanym adresie e-mail.";
                    return RedirectToAction("Index", new { message = registrationMessage });
                }
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Confirm()
        {
            var uriQuery = Request.Url.Query;
            var uriQuery2 = uriQuery.Replace("?userEmail=", "");
            var uriQuery3 = uriQuery2.Replace("?activationId", " ");

            var uriUserData = uriQuery3.Split();

            string userMail = uriUserData[0];
            Guid activationId = new Guid(uriUserData[1]);

            var selectedUser = from x in db.ListOfUsers
                               where x.Email == userMail
                               && x.ActivationId == activationId
                               select x;

            User activatedUser = selectedUser.FirstOrDefault();

            activatedUser.EmailActivated = true;
            db.SaveChanges();
            var msg = "Twój adres e-mail został zweryfikowany. Dziękujemy!";

            ViewBag.userMessage = msg;
            return View();
        }

        public void BuildEmailTemlplate(string userEmail, Guid activationId)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplates/") + "Text" + ".cshtml");
            var regInfo = db.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();
            var url = "http://localhost:57412/" + "Home/Confirm?userEmail=" + userEmail + "?activationId" + activationId;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            MailSender.BuildEmailTemlplate("Twoje konto zostało utworzone", body, regInfo.Email);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userExists = db.ListOfUsers.SingleOrDefault(x => x.Email == user.Email && x.Password == user.Password && x.EmailActivated == true);
             
            if (userExists != null)
            {
                Session["loginSuccess"] = user;
                return RedirectToAction("Index");
            }

            ViewBag.Message = $"{user.FirstName}, witamy na pokładzie.";
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            ViewBag.Message = $"Nie jesteś zalogowany.";
            Session["loginSuccess"] = null;
            return RedirectToAction("Index");
        }

    }
}