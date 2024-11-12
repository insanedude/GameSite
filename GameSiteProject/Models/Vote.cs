using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Vote
{
    public int VoteId { get; set; }
    [Display(Name = "Vote type")]
    public bool VoteType { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey("Post")]
    public int PostId { get; set; }
    public Post Post { get; set; }
}