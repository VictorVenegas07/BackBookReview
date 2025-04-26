using BookReview.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using BookReview.Domain.Entities;
using Microsoft.Extensions.Configuration;
using BookReview.Infrastructure.Extensions.Persistence;

namespace BookReview.Infrastructure.Contexts;

public class PersistenceContext : DbContext
{
    private readonly IConfiguration _config;

    public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    public DbSet<Test> Tests { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Book> Books { get; set; }




    public async Task CommitAsync()
    {
        await SaveChangesAsync().ConfigureAwait(false);

    }

    protected override void OnModelCreating(ModelBuilder? modelBuilder)
    {
        if (modelBuilder == null)
        {
            return;
        }

        modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var t = entityType.ClrType;
            if (!typeof(DomainEntity).IsAssignableFrom(t)) continue;
            modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt").HasDefaultValueSql("NOW()");
            modelBuilder.Entity(entityType.Name).Property<DateTime>("UpdatedAt").HasDefaultValueSql("NOW()");
            modelBuilder.Entity(entityType.Name).Property<DateTime?>("DeletedOn").HasDefaultValueSql("NULL");
            modelBuilder.Entity(entityType.Name).Property<bool>("IsDeleted").HasDefaultValue(false);
        }
        modelBuilder.AppendGlobalQueryFilter<ISoftDelete>(s => s.DeletedOn == null);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
