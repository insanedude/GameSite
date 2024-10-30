using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Post
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public bool IsEdited { get; set; }
    
    [ForeignKey("Thread")]
    public int? ForumThreadId { get; set; }
    public ForumThread ForumThread { get; set; }
    
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public User User { get; set; }
}