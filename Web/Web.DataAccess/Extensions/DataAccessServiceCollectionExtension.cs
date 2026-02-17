using Laraue.EfCoreTriggers.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.DataAccess.Data;
using Web.DataAccess.Repositories;
using Web.Domain.Abstractions;

namespace Web.DataAccess.Extensions;

public static class DataAccessServiceCollectionExtension
{
    public static IServiceCollection AddInformaticsDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString).UsePostgreSqlTriggers());
        
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        
        return services;
    }
}
