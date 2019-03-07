using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database.Entites;

namespace WebApplication1.Database
{
    public class DataBase: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=ec2-54-217-208-105.eu-west-1.compute.amazonaws.com;Database=dalmte4v8l4jl1;Username=evtabpfxoheehc;Port=5432;Password=60498ff30d4fded707dffc2751615d25bb1eca22cce79c5c1e727d008213d69b;SslMode=Require;Trust Server Certificate=True");
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
    }
}
