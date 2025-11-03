using Microsoft.Extensions.DependencyInjection;
using Web.Services.Mapper;

namespace Web.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInformaticsService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));

        return services;
    }
}