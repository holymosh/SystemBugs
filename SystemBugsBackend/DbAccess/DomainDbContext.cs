using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAccess
{
    public class DomainDbContext : DbContext
    {
        
        public DomainDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SberSystem>();
            var bugBuilder = modelBuilder.Entity<Bug>();
            bugBuilder.HasOne(b => b.System).WithMany(s => s.Bugs);
            bugBuilder.HasOne(b => b.DefectType).WithMany(t => t.Bugs);
            bugBuilder.HasOne(b => b.CriticalLevel).WithMany(level => level.Bugs);
            bugBuilder.HasOne(b => b.DetectionMethod).WithMany(method => method.Bugs);
            bugBuilder.HasOne(b => b.State).WithMany(state => state.Bugs);
            modelBuilder.Entity<DefectType>();
            modelBuilder.Entity<DetectionMethod>();
            modelBuilder.Entity<State>();

            base.OnModelCreating(modelBuilder);
        }
    }
}