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
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        
        return services;
    }
}