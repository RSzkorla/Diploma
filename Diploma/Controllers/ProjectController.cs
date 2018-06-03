using Diploma.BLL;
using Diploma.Models;
using Diploma.Security;
using Diploma.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diploma.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService service;

        public ProjectController() => service = new ProjectService();

        [AutorizationService]
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

                var date = Convert.ToString($"{model.Day}/{model.Month}/{model.Year}");
                var deadline = DateTime.Parse(date);

                Project project = new Project()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Promo = promo,
                    StartDate = DateTime.Now,
                    DeadLine = deadline,
                    EndDate = (DateTime)SqlDateTime.MinValue
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