namespace Web.Domain.Models;

public class SubmissionSettings
{
    public int Id { get; set; }

    public int ConferenceSettingsId { get; set; }
    public ConferenceSettings ConferenceSettings { get; set; } = null!;

    public string? IeeePdfExpressUrl { get; set; }
    public string? EasyChairUrl { get; set; }
    public string? ConferenceCode { get; set; }
    public DateTime? FinalPaperDeadline { get; set; }

    public string? IeeeTemplateUrl { get; set; }
    public string? LatexExample { get; set; }

    public int MaxPages { get; set; } = 6;
    public decimal ExtraPagePrice { get; set; } = 10;
    public int AbstractMinWords { get; set; } = 150;

    public bool IsEnabled { get; set; } = true;
}