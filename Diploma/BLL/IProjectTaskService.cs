using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diploma.Models;

namespace Diploma.BLL
{
  interface IProjectTaskService
  {
    void Create(Guid projectId, ProjectTask newpProjectTask);
    void Update(Guid projectId, Guid projectTaskId, ProjectTask updatedProjectTask);
    void Delete(Guid projectId, Guid projectTaskId);
    ProjectTask Get(Guid projectTaskId);
    IEnumerable<ProjectTask> GetAllByProject(Guid projectId);
  }
}
