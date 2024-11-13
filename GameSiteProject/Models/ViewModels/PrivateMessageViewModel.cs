using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.ViewModels;

public class PrivateMessageViewModel
{
    [Required]
    [Display(Name = "Recipient's Username")]
    public string ReceiverUsername { get; set; }

    [Required]
    [Display(Name = "Message")]
    public string Content { get; set; }
}