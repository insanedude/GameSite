using System;

namespace GameSiteProject.Models;

public class Notification
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime DateNotified { get; set; }
    
    public User User { get; set; }
}