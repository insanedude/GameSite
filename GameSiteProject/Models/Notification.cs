using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Notification
{
    public int NotificationId { get; set; }
    
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public User User { get; set; }
    
    public string Content { get; set; }
    [Display(Name = "Is seen")]
    public bool IsSeen { get; set; }
    [Display(Name = "Date notified")]
    public DateTime DateNotified { get; set; }
}