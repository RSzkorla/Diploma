using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Diploma.Models
{
  public class Project
  {
    public string Title { get; set; }
    public string Description{ get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DeadLine { get; set; }
    public DateTime EndDate { get; set; }
    public Promo Promo { get; set; }
    public List<ProjectTask> ListOfProjectTasks { get; set; }


  }
}