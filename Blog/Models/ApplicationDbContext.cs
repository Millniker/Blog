using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<PostEntity> Post { get; set; }
        public DbSet<CommentsEntity> Comments { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<UsersLikedPost> UsersLikedPosts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<PostEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<CommentsEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TagEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TokenEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<PostEntity>()
                .HasMany(t => t.Tags)
                .WithMany(p => p.Posts);

        }

    }
}
