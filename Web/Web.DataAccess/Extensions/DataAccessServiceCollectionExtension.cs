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
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString).UsePostgreSqlTriggers());
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        services.AddScoped<IAttendeeRepository, AttendeeRepository>();
        services.AddScoped<IDayRepository, DayRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IThemeRepository, ThemeRepository>();
        services.AddScoped<ITalkRepository, TalkRepository>();
        services.AddScoped<INavBarMenuRepository, NavBarMenuRepository>();
        services.AddScoped<IPageContentRepository, PageContentRepository>();
        
        return services;
    }
}
