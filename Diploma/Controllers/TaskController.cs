using Diploma.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class TaskController : Controller
    {
        [AutorizationService]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask()
        {
            return View("Index");
        }
    }
}