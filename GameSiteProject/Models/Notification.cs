using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Notification
{
    public int NotificationId { get; set; }
    
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public User User { get; set; }
    
    public string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime DateNotified { get; set; }
}