namespace Web.Services.DTOs.Committees;

public class CommitteeMemberUpdateDto
{
    public string FullName { get; set; } = string.Empty;

    public string? Position { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }

    public int Order { get; set; }
}