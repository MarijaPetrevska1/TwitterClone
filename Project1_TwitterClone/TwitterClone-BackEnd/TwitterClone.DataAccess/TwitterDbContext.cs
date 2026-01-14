using Microsoft.EntityFrameworkCore;
using TwitterClone.Domain.Entities;

namespace TwitterClone.DataAccess
{
    public class TwitterDbContext : DbContext
    {
        public TwitterDbContext(DbContextOptions<TwitterDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================
            // USER
            // ====================
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });

            // ====================
            // POST
            // ====================
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Content)
                      .IsRequired()
                      .HasMaxLength(280);

                entity.Property(p => p.CreatedAt)
                      .IsRequired();

                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.RetweetPost)
                      .WithMany()
                      .HasForeignKey(p => p.RetweetPostId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ====================
            // LIKE
            // ====================
            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasOne(l => l.User)
                      .WithMany(u => u.Likes)
                      .HasForeignKey(l => l.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(l => l.Post)
                      .WithMany(p => p.Likes)
                      .HasForeignKey(l => l.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(l => new { l.UserId, l.PostId })
                      .IsUnique();
            });

            // ====================
            // COMMENT
            // ====================
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(c => c.Content)
                      .IsRequired();

                entity.Property(c => c.CreatedAt)
                      .IsRequired();

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

