using System;
using System.Collections.Generic;

namespace GameSiteProject.Models;

public class Game
{
    public int GameId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Description { get; set; }
    public string CoverImagePath { get; set; }
    public ICollection<ForumThread> ForumThreads { get; set; }
}