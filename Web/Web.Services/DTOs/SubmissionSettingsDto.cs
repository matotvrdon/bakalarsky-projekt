namespace Web.Services.DTOs;

public class SubmissionSettingsDto
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }

    public string? IeeePdfExpressUrl { get; set; }
    public string? EasyChairUrl { get; set; }
    public string? ConferenceCode { get; set; }
    public DateTime? FinalPaperDeadline { get; set; }

    public string? IeeeTemplateUrl { get; set; }
    public string? LatexExample { get; set; }

    public int MaxPages { get; set; }
    public decimal ExtraPagePrice { get; set; }
    public int AbstractMinWords { get; set; }

    public bool IsEnabled { get; set; }
}