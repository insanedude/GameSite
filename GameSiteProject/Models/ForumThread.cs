using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class ForumThread
{
    public int ForumThreadId { get; set; }
    public string Title { get; set; }
    [ForeignKey("Game")]
    public int? GameId { get; set; }
    public Game Game { get; set; }
    
    [ForeignKey("User")]
    public int? UserId { get; set; }
    public User User { get; set; }

    [Display(Name = "Date created")]
    public DateTime DateCreated { get; set; }
    [Display(Name = "Last updated")]
    public DateTime LastUpdated { get; set; }
    [Display(Name = "Views count")]
    public int ViewsCount { get; set; }
    
    public ICollection<Post> Posts { get; set; }
    public ICollection<Tag> Tags { get; set; }
}