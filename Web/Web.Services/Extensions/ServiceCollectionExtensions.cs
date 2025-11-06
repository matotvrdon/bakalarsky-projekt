using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
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
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();
        
        QuestPDF.Settings.License = LicenseType.Community;

        return services;
    }
}