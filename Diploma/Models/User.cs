using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Mvc;

namespace Diploma.Models
{
    public class User
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "To nie jest poprawny adres e-mail.")]
        [Key]
        public string Email { get; set; }
        public bool EmailActivated { get; set; }
        public Guid ActivationId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hasło musi zawierać conajmniej 8 znaków, w tym jedną wielką i małą literę oraz cyfrę.")]

        public string Password { get; set; }

        public virtual List<Project> ListOfProjects { get; set; }

        public User()
        {
            ActivationId = Guid.NewGuid();
        }
    }
}