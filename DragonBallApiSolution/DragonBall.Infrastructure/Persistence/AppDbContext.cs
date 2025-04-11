using DragonBall.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DragonBall.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Transformation> Transformations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación 1:N entre Character y Transformation
            modelBuilder.Entity<Transformation>()
                .HasOne(t => t.Character)
                .WithMany(c => c.Transformations)
                .HasForeignKey(t => t.CharacterId);
        }
    }
    
}
