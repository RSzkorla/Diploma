using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Diploma.Database
{
  public class DiplomaDBContext: DbContext
  {
    public DiplomaDBContext(): base ("DiplomaDBConection")
    {
      
    }
    public DbSet<Models.User> ListOfUsers { get; set; }
    public DbSet<Models.Project> ListOfProjects { get; set; }
    public DbSet<Models.ProjectTask> ListOfProjectTasks { get; set; }
    public DbSet<Models.Promo> ListOfPromos { get; set; }
  }
}