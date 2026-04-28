namespace Web.Services.Options;

public class EmailSettings
{
    public string Host { get; init; } = "";
    public int Port { get; init; } = 587;
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public string FromAddress { get; init; } = "";
    public string? FromName { get; init; }
    public string LoginUrl { get; init; } = "";
    public string SecureSocketOption { get; init; } = "StartTls";
}
