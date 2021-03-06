﻿using Diploma.BLL;
using Diploma.EmailService;
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
    public class TaskController : Controller
    {
        private readonly IProjectTaskService service;

        public TaskController() => service = new ProjectTaskService();

        [AutorizationService]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTask(Guid id, string userEmail, CreateTaskVM model)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var date = Convert.ToString($"{model.Day}/{model.Month}/{model.Year}");
                        var deadline = DateTime.Parse(date);

                        ProjectTask task = new ProjectTask()
                        {
                            Title = model.Title,
                            Description = model.Description,
                            StartDate = DateTime.Now,
                            DeadLine = deadline,
                            EndDate = (DateTime)SqlDateTime.MinValue
                        };

                        service.Create(id, task);

                        return RedirectToAction("Index", "Project", new { projectId = id, userEmail = userEmail });
                    }
                    catch
                    {

                    }

                }
            }
            return RedirectToAction("Index", "Project", new
            {
                projectId = id,
                userEmail = userEmail
            });
        }

        public ActionResult DeleteTask(Guid projectId, Guid projectTaskId, string userEmail)
        {
            if (projectId != null && projectTaskId != null)
            {
                service.Delete(projectId, projectTaskId);
            }
            return RedirectToAction("Index", "Project", new { projectId = projectId, userEmail = userEmail });
        }

        public ActionResult SendPromoEmail(Guid projectId, Guid projectTaskId, string userEmail, Guid promoId)
        {
            MailSender.BuildTaskEmailToPromo(userEmail, projectTaskId, promoId);
            return RedirectToAction("Index", "Project", new { projectId = projectId, userEmail = userEmail });
        }

        public ActionResult MarkTaskAsDone(Guid projectId, Guid projectTaskId, string userEmail, Guid promoId)
        {
            var task = service.Get(projectTaskId);
            task.EndDate = DateTime.Now;
            service.Update(projectId, projectTaskId, task);
            return RedirectToAction("Index", "Project", new { projectId = projectId, userEmail = userEmail });
        }

    }
}