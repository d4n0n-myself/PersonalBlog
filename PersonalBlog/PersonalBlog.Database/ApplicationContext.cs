using Microsoft.EntityFrameworkCore;
using PersonalBlog.Core;
using PersonalBlog.Core.Entites;

namespace PersonalBlog.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            _connectionString = new ApplicationConfigurator().ConfigurationRoot["ConnectionString"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login);

            modelBuilder.Entity<Post>()
                .HasIndex(n => n.Header);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Comment> Comments { get; set; }

        private readonly string _connectionString;
    }
}