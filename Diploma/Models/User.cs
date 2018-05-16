using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diploma.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Key]
        public string Email { get; set; }
        public bool EmailActivated { get; set; }
        public Guid ActivationId { get; set; }
        public string Password { get; set; }

        public virtual List<Project> ListOfProjects { get; set; }

        public User()
        {
            ActivationId = Guid.NewGuid();
        }
    }
}