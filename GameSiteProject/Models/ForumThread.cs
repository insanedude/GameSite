using System;
using System.Collections.Generic;

namespace GameSiteProject.Models;

public class ForumThread
{
    public int ThreadId { get; set; }
    public string Title { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastUpdated { get; set; }
    public int ViewsCount { get; set; }
    
    public User User { get; set; }
    public Game Game { get; set; }
    public ICollection<Post> Posts { get; set; }
}