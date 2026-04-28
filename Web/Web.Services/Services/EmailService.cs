using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using Web.Services.Abstractions;
using Web.Services.Options;

namespace Web.Services.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;
    private const string CredentialsTemplate = """
<!doctype html>
<html lang="sk">
  <head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>INFORMATICS 2026 – Prihlasovacie údaje</title>
    <style>
      body {
        margin: 0;
        padding: 0;
        background: #f3f4f6;
        font-family: "Segoe UI", "Helvetica Neue", Arial, sans-serif;
        color: #0f172a;
      }
      .wrapper {
        width: 100%;
        background: #f3f4f6;
        padding: 32px 16px;
      }
      .container {
        max-width: 640px;
        margin: 0 auto;
        background: #ffffff;
        border-radius: 16px;
        overflow: hidden;
        border: 1px solid #e2e8f0;
        box-shadow: 0 10px 30px rgba(15, 23, 42, 0.08);
      }
      .header {
        padding: 28px 32px;
        background: linear-gradient(135deg, #e0f2fe 0%, #dbeafe 100%);
        text-align: center;
      }
      .title {
        font-size: 22px;
        font-weight: 700;
        margin: 0;
        color: #0f172a;
      }
      .subtitle {
        margin: 8px 0 0;
        font-size: 14px;
        color: #475569;
      }
      .content {
        padding: 28px 32px 8px;
      }
      .card {
        background: #f8fafc;
        border: 1px solid #e2e8f0;
        border-radius: 12px;
        padding: 16px;
        margin: 16px 0;
      }
      .label {
        font-size: 12px;
        color: #64748b;
        margin-bottom: 4px;
      }
      .value {
        font-size: 16px;
        font-weight: 600;
        color: #0f172a;
        word-break: break-word;
      }
      .cta {
        display: block;
        text-align: center;
        background: #2563eb;
        color: #ffffff;
        text-decoration: none;
        padding: 12px 16px;
        border-radius: 10px;
        font-weight: 600;
        margin: 20px 0 8px;
      }
      .steps {
        margin: 16px 0 0;
        padding-left: 18px;
        color: #475569;
        font-size: 14px;
      }
      .footer {
        padding: 20px 32px 28px;
        color: #64748b;
        font-size: 12px;
        text-align: center;
      }
      .muted {
        color: #94a3b8;
      }
    </style>
  </head>
  <body>
    <div class="wrapper">
      <div class="container">
        <div class="header">
          <h1 class="title">INFORMATICS 2026</h1>
          <p class="subtitle">Prihlasovacie údaje k účasti</p>
        </div>
        <div class="content">
          <p>Ďakujeme za registráciu. Nižšie nájdete vaše prihlasovacie údaje:</p>

          <div class="card">
            <div class="label">Email</div>
            <div class="value">{{email}}</div>
          </div>
          <div class="card">
            <div class="label">Heslo</div>
            <div class="value">{{password}}</div>
          </div>

          <a class="cta" href="{{loginUrl}}">Prihlásiť sa do systému</a>

          <p class="muted">Ak ste si registráciu nevyžiadali, tento email ignorujte.</p>

          <ol class="steps">
            <li>Prihláste sa pomocou emailu a hesla</li>
            <li>Vyplňte údaje o vašej účasti (typ účasti, ubytovanie, strava)</li>
            <li>Vygenerujte si faktúru</li>
            <li>Zaplaťte faktúru podľa pokynov</li>
          </ol>
        </div>
        <div class="footer">
          Kontakt: <span class="muted">martin@mtvrdon.com</span> · Tel: <span class="muted">+421 949 344 232</span><br />
          © 2026 INFORMATICS. Všetky práva vyhradené.
        </div>
      </div>
    </div>
  </body>
</html>
""";

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailCredentialsAsync(string email, string password)
    {
        EnsureConfigured();

        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(_settings.FromName ?? string.Empty, _settings.FromAddress));
        mailMessage.To.Add(MailboxAddress.Parse(email));
        mailMessage.Subject = "Prihlasovacie údaje na konferenciu";
        var htmlBody = BuildCredentialsHtml(email, password, _settings.LoginUrl);
        mailMessage.Body = new TextPart("html")
        {
            Text = htmlBody
        };

        using var client = new SmtpClient();
        var socketOptions = ParseSocketOptions(_settings.SecureSocketOption);
        await client.ConnectAsync(_settings.Host, _settings.Port, socketOptions);

        if (!string.IsNullOrWhiteSpace(_settings.UserName))
        {
            await client.AuthenticateAsync(_settings.UserName, _settings.Password);
        }

        await client.SendAsync(mailMessage);
        await client.DisconnectAsync(true);
    }

    private void EnsureConfigured()
    {
        if (string.IsNullOrWhiteSpace(_settings.Host))
            throw new InvalidOperationException("Email settings are not configured. Set Email:Host.");

        if (string.IsNullOrWhiteSpace(_settings.FromAddress))
            throw new InvalidOperationException("Email settings are not configured. Set Email:FromAddress.");

        if (string.IsNullOrWhiteSpace(_settings.LoginUrl))
            throw new InvalidOperationException("Email settings are not configured. Set Email:LoginUrl.");

        if (!string.IsNullOrWhiteSpace(_settings.UserName) && string.IsNullOrWhiteSpace(_settings.Password))
            throw new InvalidOperationException("Email settings are not configured. Set Email:Password.");
    }

    private static SecureSocketOptions ParseSocketOptions(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return SecureSocketOptions.Auto;

        return Enum.TryParse<SecureSocketOptions>(value, true, out var parsed)
            ? parsed
            : SecureSocketOptions.Auto;
    }

    private static string BuildCredentialsHtml(string email, string password, string loginUrl)
    {
        var safeEmail = WebUtility.HtmlEncode(email);
        var safePassword = WebUtility.HtmlEncode(password);
        var safeLoginUrl = WebUtility.HtmlEncode(loginUrl);

        return CredentialsTemplate
            .Replace("{{email}}", safeEmail, StringComparison.Ordinal)
            .Replace("{{password}}", safePassword, StringComparison.Ordinal)
            .Replace("{{loginUrl}}", safeLoginUrl, StringComparison.Ordinal);
    }
}
