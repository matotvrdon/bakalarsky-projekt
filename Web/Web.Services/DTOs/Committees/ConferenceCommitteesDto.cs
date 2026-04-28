namespace Web.Services.DTOs.Committees;

public class ConferenceCommitteesDto
{
    public int ConferenceId { get; set; }
    public string ConferenceName { get; set; } = string.Empty;

    public List<CommitteeDto> Committees { get; set; } = [];
}