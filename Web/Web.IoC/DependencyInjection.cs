using Microsoft.Extensions.DependencyInjection;
using Web.DataAccess.Extensions;
using Web.Services.Extensions;

namespace Web.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInformaticsDi(this IServiceCollection services, string connectionString)
    {
        services.AddInformaticsService();
        services.AddInformaticsDataAccess(connectionString);
        
        return services;
    }
}