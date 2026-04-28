using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ProgramItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Location { get; set; }
    public string? Speaker { get; set; }
    public string? Chair { get; set; }
    public ProgramItemType Type { get; set; }
    public int Order { get; set; }
    public List<ProgramSessionDto>? Sessions { get; set; }
}
