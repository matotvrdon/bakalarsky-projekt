using Laraue.EfCoreTriggers.PostgreSql.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.DataAccess.Repositories;

namespace Web.DataAccess.Extensions;

public static class DataAccessServiceCollectionExtension
{
    public static IServiceCollection AddInformaticsDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString).UsePostgreSqlTriggers());
        
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IConferenceRepository, ConferenceRepository>();
        services.AddScoped<IConferenceSettingsRepository, ConferenceSettingsRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<IFileManagerRepository, FileManagerRepository>();
        services.AddScoped<ISubmissionRepository, SubmissionRepository>();
        services.AddScoped<ICommitteeRepository, CommitteeRepository>();
        services.AddScoped<ISubmissionSettingsRepository, SubmissionSettingsRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IParticipantStatusRepository, ParticipantStatusRepository>();
        services.AddScoped<IParticipantStatusAssignmentRepository, ParticipantStatusAssignmentRepository>();
        
        return services;
    }
}
