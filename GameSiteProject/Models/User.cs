using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSiteProject.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string Password { get; set; }

        [Display(Name = "Profile Picture URL (Optional)")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string ProfilePicturePath { get; set; }

        public DateTime DateJoined { get; set; } = DateTime.Now;

        public string Role { get; set; }
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