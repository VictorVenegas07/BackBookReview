using BookReview.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookReview.Infrastructure.Extensions.Persistence;

public static class ContextExtensions
{
    public static IServiceCollection AddContextDatabase(this IServiceCollection svc, IConfiguration config)
    {    
        svc.AddDbContext<PersistenceContext>(opt =>
        {
            opt.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"), sqlopts =>
            {
                sqlopts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName"));
                sqlopts.MigrationsAssembly(ApiConstants.Infrastructure);
            });
        });
        return svc;
    }
}
