using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    public int Password { get; set; }
    public DateTime DateJoined { get; set; } 
    public string ProfilePicturePath { get; set; }
    public string Role { get; set; }
    public int TotalScore { get; set; }
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