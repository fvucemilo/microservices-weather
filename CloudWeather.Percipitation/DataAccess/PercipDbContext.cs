using Microsoft.EntityFrameworkCore;

namespace CloudWeather.Percipitation.DataAccess {
  public class PercipDbContext: DbContext {
    public PercipDbContext() { }
    public PercipDbContext(DbContextOptions opts) : base(opts) { }

    public DbSet<Percipitation> Percipitation { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);
      SnakeCaseIdentityTableNames(modelBuilder);
    }

    private static void SnakeCaseIdentityTableNames(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Percipitation>(b => { b.ToTable("percipitation"); });
    }
  }
}
