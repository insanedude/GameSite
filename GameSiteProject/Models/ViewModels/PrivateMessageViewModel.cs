using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models.ViewModels;

public class PrivateMessageViewModel
{
    [Required]
    [Display(Name = "Recipient's Username")]
    public string ReceiverNickname { get; set; }

    [Required]
    [Display(Name = "Message")]
    public string Content { get; set; }
}