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


            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}