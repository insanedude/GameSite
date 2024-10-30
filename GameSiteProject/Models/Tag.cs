using System.Collections.Generic;

namespace GameSiteProject.Models;

public class Tag
{
    public int TagId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<ForumThread> ForumThreads { get; set; }
}