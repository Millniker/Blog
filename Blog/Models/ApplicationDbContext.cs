using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<PostEntity> Post { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<PostEntity>().HasKey(x => x.Id);
        }

    }
}
