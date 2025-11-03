using Microsoft.Extensions.DependencyInjection;
using Web.Services.Abstractions;
using Web.Services.Mapper;
using Web.Services.Services;

namespace Web.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInformaticsService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddScoped<IConferenceService, ConferenceService>();

        return services;
    }
}