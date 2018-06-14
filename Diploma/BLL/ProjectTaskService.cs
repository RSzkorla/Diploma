using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Database;
using Diploma.Models;
using System.Data.SqlTypes;

namespace Diploma.BLL
{
    public class ProjectTaskService : IProjectTaskService
    {
        public void Create(Guid projectId, ProjectTask newProjectTask)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
                if (project != null)
                {
                    project.ListOfProjectTasks.Add(newProjectTask);
                }
                context.SaveChanges();
            }
        }

        public void Update(Guid projectId, Guid projectTaskId, ProjectTask updatedProjectTask)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
                if (project != null)
                {
                    //var projectTask = project.ListOfProjectTasks.SingleOrDefault(x => x.Id == projectTaskId);
                    //projectTask = updatedProjectTask;
                    var projectTask = project.ListOfProjectTasks.SingleOrDefault(x => x.Id == projectTaskId);
                    projectTask.Title = updatedProjectTask.Title;
                    projectTask.Description = updatedProjectTask.Description;
                    projectTask.DeadLine = updatedProjectTask.DeadLine;
                    projectTask.EndDate = updatedProjectTask.EndDate;
                }
                context.SaveChanges();
            }
        }

        public void Delete(Guid projectId, Guid projectTaskId)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
                if (project != null)
                {
                    var projectTask = project.ListOfProjectTasks.SingleOrDefault(x => x.Id == projectTaskId);
                    project.ListOfProjectTasks.Remove(projectTask);
                    context.ListOfProjectTasks.Remove(projectTask);
                }
                context.SaveChanges();
            }
        }

        public ProjectTask Get(Guid projectTaskId)
        {
            using (var context = new DiplomaDBContext())
            {
                return context.ListOfProjectTasks.SingleOrDefault(x => x.Id == projectTaskId);
            }
        }

        public IEnumerable<ProjectTask> GetAllByProject(Guid projectId)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
                if (project != null)
                    return project.ListOfProjectTasks.OrderBy(x => x.DeadLine);
                return null;
            }
        }

        public IEnumerable<ProjectTask> GetFailedByProject(Guid projectId)
        {
            using (var context = new DiplomaDBContext())
            {
                var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
                if (project != null)
                    return project.ListOfProjectTasks.Where(x => x.EndDate == (DateTime)SqlDateTime.MinValue).OrderBy(x => x.DeadLine);
                return null;
            }
        }

    }
}