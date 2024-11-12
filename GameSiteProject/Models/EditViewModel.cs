using System;
using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models;

public class EditViewModel
{
    public string Id { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string Password { get; set; }
    public string Nickname { get; set; }
    [Display(Name = "Profile Picture URL (Optional)")]
    [Url(ErrorMessage = "Please enter a valid URL.")]
    public string ProfilePicturePath { get; set; }
    public DateTime DateJoined { get; set; } = DateTime.Now;
    public int TotalScore { get; set; }
    public string UserInformation { get; set; }
}