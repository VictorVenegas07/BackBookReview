using BookReview.Infrastructure.Extensions.Cors;
using BookReview.Infrastructure.Extensions.Feature;
using BookReview.Infrastructure.Extensions.Jwt;
using BookReview.Infrastructure.Extensions.Mapper;
using BookReview.Infrastructure.Extensions.Mediator;
using BookReview.Infrastructure.Extensions.Persistence;
using BookReview.Infrastructure.Extensions.Swagger;
using BookReview.Infrastructure.Extensions.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookReview.Infrastructure.Extensions;

public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddSwagger()
            .AddValidation()
            .AddMediator()
            .AddJsonWebToken()
            .AddMapper()
            .AddContextDatabase(config)
            .AddCorsPolicy(config)
            .AddPersistence(config)
            .AddHttpContextAccessor()
            .AddFeature(config);
    }

    public static void UseInfrastructure(this IApplicationBuilder builder, IWebHostEnvironment env)
    {
        builder
            .UseSwagger(env)
            .UseCorsPolicy();
    }
}
