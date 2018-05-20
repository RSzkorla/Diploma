using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.ViewModels
{
    public class UserRegistrationVM
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "To nie jest poprawny adres e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hasło musi zawierać conajmniej 8 znaków, w tym jedną wielką i małą literę oraz cyfrę.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą się zgadzać!")]
        public string ConfirmPassword { get; set; }

    }
}