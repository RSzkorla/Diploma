using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Diploma.Models
{
  public class UndoneTaskPerProject
  {
    public Project Project;
    public List<ProjectTask> UndoneProjectTasks;
  }
}