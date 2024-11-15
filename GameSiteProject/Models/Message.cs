using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Message
{
    public int MessageId { get; set; }
    
    [ForeignKey("SenderId")]
    [InverseProperty("SentMessages")]
    public string? SenderId { get; set; }
    public User Sender { get; set; }
    
    [ForeignKey("ReceiverId")]
    [InverseProperty("ReceivedMessages")]
    public string? ReceiverId { get; set; }
    public User? Receiver { get; set; }
    
    public string Content { get; set; }
    [Display(Name = "Date Sent")]
    public DateTime DateSent { get; set; }
    [Display(Name = "Is read")]
    public bool IsRead { get; set; }
    
    [ForeignKey("ForumThreadId")]
    public int? ForumThreadId { get; set; }
    public ForumThread? ForumThread { get; set; }
}