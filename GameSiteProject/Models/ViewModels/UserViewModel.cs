using System;

namespace GameSiteProject.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string ProfilePicturePath { get; set; }
    public string UserInformation { get; set; }
    public int TotalScore { get; set; }
    public DateTime DateJoined { get; set; } = DateTime.Now;
}