using System;
using System.Collections.Generic;

namespace GameSiteProject.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int PasswordHash { get; set; }
    public DateTime DateJoined { get; set; } 
    public string ProfilePicturePath { get; set; }
    public string Role { get; set; }
    public int TotalScore { get; set; }
    public string UserInformation { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
    public ICollection<ForumThread> ForumThreads { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Notification> Notifications { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public ICollection<Message> SentMessages { get; set; }
    public ICollection<Message> ReceivedMessages { get; set; }
}