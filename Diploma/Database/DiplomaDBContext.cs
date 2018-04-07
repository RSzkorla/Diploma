using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diploma.Database
{
  public class DiplomaDBContext: DbContext
  {
    public DbSet<Models.User> ListOfProjects { get; set; }
    public DbSet<Models.Project> ListOfProjectTasks { get; set; }
  }
}