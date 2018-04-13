using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Diploma.Models
{
  public class Project
  {
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description{ get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DeadLine { get; set; }
    public DateTime EndDate { get; set; }
    public Promo Promo { get; set; }
    public virtual List<ProjectTask> ListOfProjectTasks { get; set; }

    public Project()
    {
      Id = Guid.NewGuid();
    }

  }
}