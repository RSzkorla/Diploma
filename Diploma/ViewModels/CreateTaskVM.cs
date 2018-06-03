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

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

    }
}