using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Database;
using Diploma.Models;
using System.Data.SqlTypes;
using System.Data.Entity;

namespace Diploma.BLL
{
    public class ProjectService : IProjectService
    {
        public void Create(Project project, Promo promo, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var prom = context.ListOfPromos.Where(x => x.Id == promo.Id).FirstOrDefault();
                if (prom == null)
                {
                    context.ListOfPromos.Add(promo);
                    context.SaveChanges();
                }

                context.ListOfProjects.Add(project);

                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                user?.ListOfProjects.Add(project);

                context.SaveChanges();
            }
        }

        public void Update(Guid id, Project updatedProject, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (user != null)
                {
                    var project = user.ListOfProjects.SingleOrDefault(x => x.Id == id);
                    if (project != null)
                        project = updatedProject;
                }
                context.SaveChangesAsync();
            }
        }

        public void Delete(Guid id, string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var proj = context.ListOfProjects.Where(x => x.Id == id).FirstOrDefault();
                var taskList = context.ListOfProjects.Where(x => x.Id == id).FirstOrDefault().ListOfProjectTasks;
                var user = context.ListOfUsers.Where(x => x.Email == userEmail).FirstOrDefault();

                if (user != null)
                {
                    proj.Promo = null;

                    if (taskList != null)
                    {
                        context.ListOfProjectTasks.RemoveRange(taskList);
                    }
                    proj.ListOfProjectTasks.RemoveAll(x => x.Id != null);

                    context.ListOfProjects.Attach(proj);
                    user.ListOfProjects.Remove(proj);
                    context.ListOfProjects.Remove(proj);
                    context.SaveChanges();
                }
            }
        }

        public Project GetById(Guid id)
        {
            using (var context = new DiplomaDBContext())
            {
                return context.ListOfProjects.Where(x => x.Id == id).Include(x => x.Promo).FirstOrDefault();
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (var context = new DiplomaDBContext())
            {
                return context.ListOfProjects;
            }
        }

        public IEnumerable<Project> GetAllByUserId(string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var singleOrDefault = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (singleOrDefault != null)
                    return singleOrDefault.ListOfProjects;
                return null;
            }
        }

        public UndoneTaskPerProject GetFailedTasks(Guid id)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == id);
                var taskService = new ProjectTaskService();
                var listOfUndoneTasks = taskService.GetAllByProject(id).Where(x => x.EndDate == (DateTime)SqlDateTime.MinValue
                && DateTime.Compare(x.DeadLine.Date, DateTime.Now) <= 0).OrderBy(x => x.DeadLine).ToList();
                return new UndoneTaskPerProject() { Project = project, UndoneProjectTasks = listOfUndoneTasks };
            }

        }


        public IEnumerable<UndoneTaskPerProject> GetAllFailedTasksByUser(string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (user != null)
                    foreach (var project in user.ListOfProjects)
                    {
                        yield return GetFailedTasks(project.Id);
                    }
            }

        }

        public UndoneTaskPerProject GetUndoneTasks(Guid id)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == id);
                var taskService = new ProjectTaskService();
                var listOfUndoneTasks = taskService.GetAllByProject(id).Where(x => x.EndDate == (DateTime)SqlDateTime.MinValue).OrderBy(x => x.DeadLine).ToList();
                return new UndoneTaskPerProject() { Project = project, UndoneProjectTasks = listOfUndoneTasks };
            }

        }

        public IEnumerable<UndoneTaskPerProject> GetAllUndoneTasksByUser(string userEmail)
        {
            using (var context = new DiplomaDBContext())
            {
                var user = context.ListOfUsers.SingleOrDefault(x => x.Email == userEmail);
                if (user != null)
                    foreach (var project in user.ListOfProjects)
                    {
                        yield return GetUndoneTasks(project.Id);
                    }
            }
        }


        public int[] GetStatisticsDoneUndoneFailedTasks(Guid projectId)
        {
            using (var context = new DiplomaDBContext())
            {
                var taskService = new ProjectTaskService();
                var doneTasksCount = taskService
                  .GetAllByProject(projectId)
                  .Count(x => x.EndDate != (DateTime)SqlDateTime.MinValue);
                var undoneTasksCount = taskService
                  .GetAllByProject(projectId)
                  .Count(x => x.EndDate == (DateTime)SqlDateTime.MinValue);
                var failedTasksCount = taskService
                  .GetAllByProject(projectId)
                  .Count(x => x.DeadLine < DateTime.Today && x.EndDate == (DateTime)SqlDateTime.MinValue);
                return new[] { doneTasksCount, undoneTasksCount, failedTasksCount };
            }
        }
    }
}