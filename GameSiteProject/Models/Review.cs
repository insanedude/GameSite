using System;

namespace GameSiteProject.Models;

public class Review
{
    public int ReviewId { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public short Rating { get; set; }
    public DateTime DatePosted { get; set; }
    
    public User User { get; set; }
    public Game Game { get; set; }
}