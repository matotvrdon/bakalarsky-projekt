using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Web.Services.Abstractions;
using Web.Services.Mapping;
using Web.Services.Services;

namespace Web.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInformaticsService(this IServiceCollection services)
    {
                
        QuestPDF.Settings.License = LicenseType.Community;
        
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        return services;
    }
}
