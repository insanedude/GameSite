using System;

namespace GameSiteProject.Models;

public class Post
{
    public int PostId { get; set; }
    public int ThreadId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime DatePosted { get; set; }
    public bool IsEdited { get; set; }
    
    public ForumThread ForumThread { get; set; }
    public User User { get; set; }
}