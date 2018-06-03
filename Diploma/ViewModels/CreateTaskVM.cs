using Diploma.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.ViewModels
{
    public class CreateTaskVM
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DeadLine { get; set; }
        public TaskTag Tag { get; set; }
    }
}