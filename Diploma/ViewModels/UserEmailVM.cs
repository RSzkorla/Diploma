using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diploma.ViewModels
{
    public class UserEmailVM
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "To nie jest poprawny adres e-mail.")]
        public string Email { get; set; }

    }
}