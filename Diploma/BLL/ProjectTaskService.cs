using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Diploma.Database;
using Diploma.Models;

namespace Diploma.BLL
{
  public class ProjectTaskService : IProjectTaskService
  {
    public void Create(Guid projectId, ProjectTask newpProjectTask)
    {
      using (var context = new DiplomaDBContext())
      {
        var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
        if (project != null)
        {
          project.ListOfProjectTasks.Add(newpProjectTask);
        }
        context.SaveChangesAsync();
      }
    }

    public void Update(Guid projectId, Guid projectTaskId, ProjectTask updatedProjectTask)
    {
      using (var context = new DiplomaDBContext())
      {
        var project = context.ListOfProjects.SingleOrDefault(x => x.Id == projectId);
        if (project != null)
        {
          var projectTask = project.ListOfProjectTasks.SingleOrDefault(x => x.Id == projectTaskId);
          projectTask = updatedProjectTask;
        }
        context.SaveChangesAsync();
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
        }
        context.SaveChangesAsync();
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
          return project.ListOfProjectTasks;
        return null;
      }
    }
  }
}