using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }
    }
}