namespace GameSiteProject.Models;

public class Vote
{
    public int VoteId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int VoteType { get; set; }
    
    public User User { get; set; }
    public Review Review { get; set; }
    public Post Post { get; set; }
}