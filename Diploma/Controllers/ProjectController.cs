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
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult CreateProject(CreateProjectVM model, string userEmail)
        {
            if (ModelState.IsValid)
            {
                var promo = new Promo() { Name = model.PromoName, Email = model.PromoEmail };
                using (Database.DiplomaDBContext context = new Database.DiplomaDBContext())
                {
                    context.ListOfPromos.Add(promo);
                    context.SaveChanges();


                    Project project = new Project()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        Promo = promo,
                        StartDate = DateTime.Now,
                        DeadLine = DateTime.Now,
                        EndDate = DateTime.Now,
                    };

                    context.ListOfProjects.Add(project);
                    context.ListOfUsers.Where(x=>x.Email==userEmail).First().ListOfProjects.Add(project);
                    context.SaveChanges();
                }
                return RedirectToAction("Dashboard", "User");
            }
            return RedirectToAction("Dashboard", "User");
        }
    }
}