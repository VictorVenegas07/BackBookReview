using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookReview.Infrastructure.Extensions.Mapper;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection svc)
    {
        svc.AddAutoMapper(Assembly.Load(ApiConstants.ApplicationProject));
        return svc;
    }
}
