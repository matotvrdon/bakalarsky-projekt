using Laraue.EfCoreTriggers.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.DataAccess.Data;

namespace Web.DataAccess.Extensions;

public static class DataAccessServiceCollectionExtension
{
    public static IServiceCollection AddInformaticsDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString).UsePostgreSqlTriggers());
        
        return services;
    }
}
