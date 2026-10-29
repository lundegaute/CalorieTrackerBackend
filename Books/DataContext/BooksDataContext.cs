using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Books.Models;
using Books.Models.Interfaces;

namespace Books.DataContext;

public class BooksDataContext : DbContext
{
    public BooksDataContext(DbContextOptions<BooksDataContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }


    // These two make sure every object is saved or updated with correct CreatedAt and UpdatedAt properties
    public override Task<int> SaveChangesAsync(bool accepAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateAuditTimestamps();
        return base.SaveChangesAsync(accepAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges(bool accepAllChangesOnSuccess)
    {
        UpdateAuditTimestamps();
        return base.SaveChanges(accepAllChangesOnSuccess);
    }

    private void UpdateAuditTimestamps()
    {
        var entries = ChangeTracker.Entries<IAuditable>();
        var now = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
