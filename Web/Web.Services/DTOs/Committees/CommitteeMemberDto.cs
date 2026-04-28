namespace Web.Services.DTOs.Committees;

public class CommitteeMemberDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? Position { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }

    public int Order { get; set; }
}