using Diploma.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.ViewModels
{
    public class CreateProjectVM
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PromoName { get; set; }

        public string PromoEmail { get; set; }
    }
}