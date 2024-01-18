using Microsoft.EntityFrameworkCore;
using Stroytorg.Domain.Data.Entities.Common;
using Stroytorg.Domain.Extensions;
using Stroytorg.Infrastructure.Configuration.Interfaces;

namespace Stroytorg.Domain.Data.Entities;

public class StroytorgDbContext : DbContext, IStroytorgDbContext
{
    private readonly IDatabaseConnectionString? databaseConnectionString;

    public StroytorgDbContext()
    {
    }

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
        Database.Migrate();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        try 
        { 
            await SaveChangesAsync(cancellationToken); 
        }
        catch (Exception) 
        {
            Rollback();
        }
    }

    public void Rollback()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            entry.State = EntityState.Unchanged;
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(databaseConnectionString?.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyStroytorgConfigurations();
    }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Material> Material { get; set; }

    public virtual DbSet<OrderMaterialMap> Order { get; set; }

    public virtual DbSet<OrderMaterialMap> OrderMaterialMap { get; set; }
}
