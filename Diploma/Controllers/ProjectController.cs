using Diploma.BLL;
using Diploma.Models;
using Diploma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class ProjectController : Controller
    {
        IProjectService service = new ProjectService();
        // GET: Project
        public ActionResult Index(Guid projectId, string userEmail)
        {
            var project = service.GetById(projectId);
            ViewBag.Project = project;
            ViewBag.Id = projectId;
            ViewBag.UserEmail = userEmail;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(CreateProjectVM model, string userEmail)
        {
            if (ModelState.IsValid)
            {
                var promo = new Promo() { Name = model.PromoName, Email = model.PromoEmail };

                Project project = new Project()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Promo = promo,
                    StartDate = DateTime.Now,
                    DeadLine = DateTime.Now,
                    EndDate = DateTime.Now,
                };

                service.Create(project, promo, userEmail);
            }
            return RedirectToAction("Dashboard", "User");
        }

        [HttpPost]
        public ActionResult DeleteProject(Guid id, string userEmail)
        {
            if (id != null)
            {
                var project = service.GetById(id);
                service.Delete(project, userEmail);
            }
            return RedirectToAction("Dashboard", "User");
        }
    }
}