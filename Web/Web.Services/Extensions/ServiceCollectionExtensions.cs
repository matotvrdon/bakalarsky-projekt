using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Web.Domain.Models;
using Web.Services.Abstractions;
using Web.Services.Mapping;
using Web.Services.Services;
using Web.Services.Validation;

namespace Web.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInformaticsService(this IServiceCollection services)
    {
                
        QuestPDF.Settings.License = LicenseType.Community;

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();
        
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IConferenceService, ConferenceService>();
        services.AddScoped<IConferenceSettingsService, ConferenceSettingsService>();
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<IFileManagerService, FileManagerService>();
        services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<IProgramPdfGenerator, ProgramPdfGenerator>();
        services.AddScoped<ICommitteeService, CommitteeService>();
        services.AddScoped<ISubmissionSettingsService, SubmissionSettingsService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IInvoicePdfGenerator, InvoicePdfGenerator>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IInvoicePdfStorageService, InvoicePdfStorageService>();
        services.AddScoped<IParticipantStatusService, ParticipantStatusService>();
        services.AddScoped<IParticipantStatusAssignmentService, ParticipantStatusAssignmentService>();
        
        services.AddTransient<IEmailService, EmailService>();
        
        return services;
    }
}
