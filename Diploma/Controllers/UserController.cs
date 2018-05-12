using Diploma.Database;
using Diploma.Models;
using Diploma.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class UserController : Controller
    {
        private DiplomaDBContext db = new DiplomaDBContext();

        //z bazą danych
        [AutorizationService]
        public ActionResult Dashboard()
        {
            string eMail = Session["user"].ToString();
            User currentUser = db.ListOfUsers.Where(x => x.Email == eMail).FirstOrDefault();
            ViewBag.User = currentUser;
            return View(ViewBag.User);
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
    }
}