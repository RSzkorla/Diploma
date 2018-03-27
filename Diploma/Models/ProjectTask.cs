using System;
using System.Web.UI.WebControls;

namespace Diploma.Models
{
  public class ProjectTask
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DeadLine { get; set; }
    public TaskTag Tag { get; set; }
  }
}