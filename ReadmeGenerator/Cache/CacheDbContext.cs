using Microsoft.EntityFrameworkCore;

namespace Quera.Cache;

public class CacheDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<CacheProblem> Problems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CacheProblem>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
        });
    }
}