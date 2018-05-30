using Diploma.Database;
using Diploma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.BLL
{
    public class TaskService
    {
        public void Create(ProjectTask task, Project currentProject)
        {
            using (DiplomaDBContext context = new DiplomaDBContext())
            {
                currentProject.ListOfProjectTasks.Add(task);
                context.ListOfProjectTasks.Add(task);
            }
        }
    }
}