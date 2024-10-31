using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class User
{
    public int UserId { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public string Password { get; set; }
    [Display(Name = "Date joined")]
    public DateTime DateJoined { get; set; }
    [Display(Name = "PFP Path")]
    public string ProfilePicturePath { get; set; }
    public string Role { get; set; }
    [Display(Name = "Total Score")]
    public int TotalScore { get; set; }
    [Display(Name = "User info")]
    public string UserInformation { get; set; }

    public ICollection<ForumThread> ForumThreads { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<Vote> Votes { get; set; }
    [InverseProperty("Sender")]
    public ICollection<Message> SentMessages { get; set; }
    [InverseProperty("Receiver")]
    public ICollection<Message> ReceivedMessages { get; set; }
}