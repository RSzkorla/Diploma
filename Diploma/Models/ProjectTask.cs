using System;
using System.ComponentModel.DataAnnotations;
using System.Web.UI.WebControls;

namespace Diploma.Models
{
  public class ProjectTask
  {
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DeadLine { get; set; }
    public TaskTag Tag { get; set; }

    public ProjectTask()
    {
      Id = Guid.NewGuid();
    }
  }
}