using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameSiteProject.Models
{
    public class GameSiteDbContext : IdentityDbContext<User>
    {
        public GameSiteDbContext(DbContextOptions<GameSiteDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure the one-to-many relationship between Message and User (SentMessages)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender) // Message has one Sender (User)
                .WithMany(u => u.SentMessages) // User has many SentMessages
                .HasForeignKey(m => m.SenderId) // Foreign key for Sender
                .OnDelete(DeleteBehavior.ClientCascade); // Optional: configure delete behavior

            // Configure the one-to-many relationship between Message and User (ReceivedMessages)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver) // Message has one Receiver (User)
                .WithMany(u => u.ReceivedMessages) // User has many ReceivedMessages
                .HasForeignKey(m => m.ReceiverId) // Foreign key for Receiver
                .OnDelete(DeleteBehavior.ClientCascade); // Optional: configure delete behavior
        }
    }
}