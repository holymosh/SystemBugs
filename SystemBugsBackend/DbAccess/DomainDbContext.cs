using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAccess
{
    public class DomainDbContext : DbContext
    {
        
        public DomainDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SberSystem>();
            modelBuilder.Entity<Bug>();
            modelBuilder.Entity<DefectType>();
            modelBuilder.Entity<DetectionMethod>();
            modelBuilder.Entity<State>();

            base.OnModelCreating(modelBuilder);
        }
    }
}