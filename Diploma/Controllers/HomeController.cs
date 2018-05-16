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
    }
}