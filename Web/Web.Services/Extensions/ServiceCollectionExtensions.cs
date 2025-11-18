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
        services.AddAutoMapper(typeof(AttendeeProfile));
        services.AddAutoMapper(typeof(ConferenceProfile));
        services.AddAutoMapper(typeof(CustomerProfile));
        services.AddAutoMapper(typeof(InvoiceItemProfile));
        services.AddAutoMapper(typeof(InvoiceProfile));
        services.AddAutoMapper(typeof(SupplierProfile));
        services.AddAutoMapper(typeof(DayProfile));
        services.AddAutoMapper(typeof(SessionProfile));
        services.AddAutoMapper(typeof(TalkProfile));
        services.AddAutoMapper(typeof(ThemeProfile));

        services.AddScoped<IConferenceService, ConferenceService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvoiceItemService, InvoiceItemService>();
        services.AddScoped<IAttendeeService, AttendeeService>();
        
        QuestPDF.Settings.License = LicenseType.Community;

        return services;
    }
}