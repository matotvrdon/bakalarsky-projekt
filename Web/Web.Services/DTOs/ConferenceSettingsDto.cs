namespace Web.Services.DTOs;

public class ConferenceSettingsDto
{
    public int ConferenceId { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? WelcomeMessage { get; set; }
    public decimal FeeSpeaker { get; set; }
    public decimal FeeParticipant { get; set; }
    public decimal FeeStudent { get; set; }
    public decimal EarlyBirdPercent { get; set; }
}
