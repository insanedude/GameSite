using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models;

public class Game
{
    public int GameId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    [Display(Name = "Cover Image Path")]
    public string CoverImagePath { get; set; }
    
    public ICollection<Tag> Tags { get; set; }
    public ICollection<ForumThread> ForumThreads { get; set; }
}