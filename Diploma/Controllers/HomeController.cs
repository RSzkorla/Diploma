using Diploma.BLL;
using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using Diploma.Security;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {
        private DiplomaDBContext db = new DiplomaDBContext();

        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            if (Session["user"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Dashboard", "User");
            }
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
                    AccountService.Add(newUser);
                    registrationMessage = "Na podany adres e-mail została wysłana wiadomość weryfikacjyjna.";
                    MailSender.BuildEmailTemlplate(newUser.Email, newUser.ActivationId);
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
            AccountService.ActivateAccount(Request.Url.Query);
            var msg = "Twój adres e-mail został zweryfikowany. Dziękujemy!";

            ViewBag.userMessage = msg;
            return View();
        }
    }
}