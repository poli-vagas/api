using Microsoft.EntityFrameworkCore;
using PoliVagas.Core.Domain;

namespace PoliVagas.Core.Infrastructure.Persistence;

public class SqlContext : DbContext
{
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<IntegrationAgent> Agents { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
            .OwnsOne(o => o.Benefits);

        modelBuilder.Entity<Job>()
            .OwnsOne(o => o.Requirements);

        modelBuilder.Entity<Job>()
            .OwnsOne(o => o.Contact);

        modelBuilder.Entity<Job>()
            .HasMany(j => j.Courses)
            .WithMany();

        modelBuilder.Entity<Notification>()
            .OwnsOne(n => n.Filter);
    }
}