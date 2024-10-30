using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSiteProject.Models;

public class Message
{
    public int MessageId { get; set; }
    
    [ForeignKey("SenderId")]
    [InverseProperty("SentMessages")]
    public int? SenderId { get; set; }
    public User Sender { get; set; }
    
    [ForeignKey("ReceiverId")]
    [InverseProperty("ReceivedMessages")]
    public int? ReceiverId { get; set; }
    public User Receiver { get; set; }
    
    public string Content { get; set; }
    public DateTime DateSent { get; set; }
    public bool IsRead { get; set; }
}