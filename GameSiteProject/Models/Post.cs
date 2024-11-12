using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Post
{
    public int PostId { get; set; }
    public string Content { get; set; }
    [Display(Name = "Date posted")]
    public DateTime DatePosted { get; set; }
    [Display(Name = "Is edited")]
    public bool IsEdited { get; set; }
    
    [ForeignKey("Thread")]
    public int? ForumThreadId { get; set; }
    public ForumThread ForumThread { get; set; }
    
    [ForeignKey("User")]
    public string? UserId { get; set; }
    public User User { get; set; }
}