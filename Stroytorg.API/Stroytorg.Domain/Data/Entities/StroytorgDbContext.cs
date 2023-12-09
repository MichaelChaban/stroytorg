using Microsoft.EntityFrameworkCore;
using Stroytorg.Infrastructure.Infrastructure;

namespace Stroytorg.Domain.Data.Entities;

public class StroytorgDbContext : DbContext, IStroytorgDbContext 
{
    private readonly IDatabaseConnectionString databaseConnectionString;

    public StroytorgDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public StroytorgDbContext(IDatabaseConnectionString databaseConnectionString)
    {
        this.databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
    }

    public void Migrate()
    {
        this.Database.Migrate();
    }

    public void Commit() => this.SaveChanges();

    public void Rollback()
    {
        foreach (var entry in this.ChangeTracker.Entries())
        {
            entry.State = EntityState.Unchanged;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(databaseConnectionString.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StroytorgDbContext).Assembly);
    }
}
