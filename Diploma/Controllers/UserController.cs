using Diploma.BLL;
using Diploma.Database;
using Diploma.EmailService;
using Diploma.Models;
using Diploma.Security;
using Diploma.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        private DiplomaDBContext db = new DiplomaDBContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(UserRegistrationVM newUser)
        {
            string registrationMessage = null;

            if (ModelState.IsValid)
            {
                try
                {
                    AccountService.Register(newUser);
                    registrationMessage = "Na podany adres e-mail została wysłana wiadomość weryfikacjyjna.";
                    MailSender.BuildRegistrationEmailTemlplate(newUser.Email);
                    return RedirectToAction("Index", "Home", new { message = registrationMessage });
                }
                catch (Exception)
                {
                    registrationMessage = "Istnieje już konto o podanym adresie e-mail.";
                    return RedirectToAction("Index", "Home", new { message = registrationMessage });
                }
            }
            else
            {
                return View("../Home/Index");
            }
        }

        public ActionResult Confirm(string userEmail, string hash)
        {
            if (AccountService.GetByKey(userEmail) != null && AccountService.GenerateUserHash(AccountService.GetByKey(userEmail)) == hash)
            {
                AccountService.ActivateAccount(userEmail, hash);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", new { message = userEmail });
            }
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userExists = db.ListOfUsers.SingleOrDefault(x => x.Email == user.Email && x.EmailActivated == true);

            if (userExists != null && PasswordHash.ValidatePassword(user.Password, userExists.Password) == true)
            {
                Session["loginSuccess"] = true;
                Session["user"] = user.Email;
                return RedirectToAction("Dashboard", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["loginSuccess"] = null;
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(UserPasswordVM user, string userEmail, string hash)
        {
            if (ModelState.IsValid)
            {
                AccountService.ChangePassword(userEmail, user.Password);
                return RedirectToAction("Index", "Home", new { message = $"{userEmail},{user.Password}"});
            }
            else
            {
                return RedirectToAction("PasswordRecovery", "User", new { Password = user.Password, ConfirmPassword = user.ConfirmPassword, userEmail = userEmail, hash = hash });
            }
        }

        public ActionResult PasswordRecovery(UserPasswordVM user, string userEmail, string hash)
        {
            if (AccountService.GetByKey(userEmail) == null || AccountService.GenerateUserHash(AccountService.GetByKey(userEmail)) != hash)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Email = userEmail;
                ViewBag.Hash = hash;
                ViewBag.User = user;


                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordRecovery(UserEmailVM userEmail)
        {
            if (ModelState.IsValid)
            {
                MailSender.BuildPasswordRecoveryEmailTemplate(userEmail.Email);
                string registrationMessage = "Na podany adres e-mail została wysłana wiadomość z przypomnieniem hasła.";
                return RedirectToAction("ForgotPassword", "User", new { message = registrationMessage });
            }
            else
            {
                return RedirectToAction("ForgotPassword", "User");
            }
        }


        public ActionResult ForgotPassword(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        //z bazą danych
        [AutorizationService]
        public ActionResult Dashboard(User user)
        {
                string eMail = Session["user"].ToString();
                User currentUser = db.ListOfUsers.Where(x => x.Email == eMail).FirstOrDefault();
                ViewBag.User = currentUser;
                return View();
        }

        //bez bazy
        //public ActionResult Dashboard()
        //{
        //    User currentUser = new User();
        //    currentUser.Email = "test@test.pl";
        //    currentUser.FirstName = "Test";
        //    currentUser.LastName = "Testowy";

        //    Project project1 = new Project();
        //    project1.Title = "Projekt testowy";
        //    project1.StartDate = DateTime.Now;

        //    currentUser.ListOfProjects = new List<Project>();
        //    currentUser.ListOfProjects.Add(project1);

        //    ViewBag.User = currentUser;

        //    return View(ViewBag.User);
        //}
    }
}