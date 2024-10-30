using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Vote
{
    public int VoteId { get; set; }
    public int VoteType { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("Post")]
    public int PostId { get; set; }
    public Post Post { get; set; }
}