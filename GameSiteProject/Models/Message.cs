using System;

namespace GameSiteProject.Models;

public class Message
{
    public int MessageId { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime DateSent { get; set; }
    public bool IsRead { get; set; }
    
    public User Sender { get; set; }
    public User Receiver { get; set; }
}