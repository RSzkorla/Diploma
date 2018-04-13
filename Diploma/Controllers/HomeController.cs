using Diploma.Database;
using Diploma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class HomeController : Controller
    {

        private DiplomaDBContext db = new DiplomaDBContext();
        private static List<User> usersList = new List<User>();

        public ActionResult Index()
        {
            return View(usersList);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(User newUser)
        {
            db.ListOfUsers.Add(newUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}