using System;
using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models;

public class RegistrationViewModel
{
    [Required(ErrorMessage = "Nickname is required.")]
    [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
    public string Password { get; set; }
    public string ProfilePicturePath { get; set; }
    public string UserInformation { get; set; }

    public DateTime DateJoined { get; set; } = DateTime.Now;
}