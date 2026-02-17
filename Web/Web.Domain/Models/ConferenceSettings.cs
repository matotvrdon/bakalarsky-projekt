namespace Web.Domain.Models;

public class ConferenceSettings
{
    public int Id { get; set; }
    public int ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactPhone { get; set; }
    public string? WelcomeMessage { get; set; }
    public decimal FeeSpeaker { get; set; }
    public decimal FeeParticipant { get; set; }
    public decimal FeeStudent { get; set; }
    public decimal EarlyBirdPercent { get; set; }
}