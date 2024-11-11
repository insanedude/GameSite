using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GameSiteProject.Models
{
    public class User: IdentityUser
    {
        [Required]
        public string Nickname { get; set; }
        [Display(Name = "Profile Picture URL (Optional)")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string ProfilePicturePath { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;
        public int TotalScore { get; set; }
        public string UserInformation { get; set; }

        public ICollection<ForumThread> ForumThreads { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }
}