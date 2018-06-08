using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma.Models;

namespace Diploma.BLL
{
    interface IProjectService
    {
        void Create(Project project, Promo promo, string userEmail);
        void Update(Guid id, Project updatedProject, string userEmail);
        void Delete(Guid id, string userEmail);
        Project GetById(Guid id);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetAllByUserId(string userEmail);
        IEnumerable<UndoneTaskPerProject> GetAllUndoneTasksByUser(string userEmail);
        UndoneTaskPerProject GetUndoneTasks(Guid id);


    }
}
