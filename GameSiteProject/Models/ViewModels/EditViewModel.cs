using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models.ViewModels;

public class EditViewModel
{
    public string Id { get; set; }

    [Required]
    public string Nickname { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ProfilePicturePath { get; set; }
    public string UserInformation { get; set; }
}