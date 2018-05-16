using System.ComponentModel.DataAnnotations;

namespace Diploma.ViewModels
{
    public class UserPasswordVM
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [MinLength(8, ErrorMessage = "Hasło musi mieć conajmniej 8 znaków.")]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hasło musi zawierać conajmniej 8 znaków, w tym jedną wielką i małą literę oraz cyfrę.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła muszą się zgadzać!")]
        public string ConfirmPassword { get; set; }

    }
}