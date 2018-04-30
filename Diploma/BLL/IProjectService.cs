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
    void Create(Project project);
    void Update(Project project);
    void Delete(Project project);
    void DeleteById(Guid id);
    Project GetById(Guid id);
    IEnumerable<Project> GetAll();
    IEnumerable<Project> GetAllByUserId(Guid userGuid);
  }
}
