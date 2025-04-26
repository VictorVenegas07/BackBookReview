using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookReview.Infrastructure.Contexts;
using BookReview.Infrastructure.Extensions;

namespace BookReview.Api;
/// <summary>
/// Factory for creating the PersistenceContext.
/// </summary>
public class PersistenceContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
{
    /// <summary>
    /// Creates a new instance of the PersistenceContext.
    /// </summary>
    public PersistenceContext CreateDbContext(string[] args)
    {
        var Config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
        DotNetEnv.Env.Load();
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
        Console.WriteLine($"Connection string: {connectionString}");
        var optionsBuilder = new DbContextOptionsBuilder<PersistenceContext>();
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), sqlopts =>
        {
            sqlopts.MigrationsHistoryTable("_MigrationHistory", Config.GetValue<string>("SchemaName"));
            sqlopts.MigrationsAssembly(ApiConstants.Infrastructure);
        });

        return new PersistenceContext(optionsBuilder.Options, Config);
    }
}
