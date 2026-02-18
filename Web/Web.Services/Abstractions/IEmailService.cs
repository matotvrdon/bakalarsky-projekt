namespace Web.Services.Abstractions;

public interface IEmailService
{
    Task SendEmailCredentialsAsync(string email, string password);
}
