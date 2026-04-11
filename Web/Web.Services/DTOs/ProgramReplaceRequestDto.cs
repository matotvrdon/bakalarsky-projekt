using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ProgramReplaceRequestDto
{
    public List<ProgramDayReplaceDto> ProgramDays { get; set; } = [];
}

public class ProgramDayReplaceDto
{
    public string Label { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int Order { get; set; }
    public List<ProgramItemReplaceDto> ProgramItems { get; set; } = [];
}

public class ProgramItemReplaceDto
{
    public string Title { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Location { get; set; }
    public string? Speaker { get; set; }
    public string? Chair { get; set; }
    public ProgramItemType Type { get; set; }
    public int Order { get; set; }
    public List<ProgramSessionReplaceDto> Sessions { get; set; } = [];
}

public class ProgramSessionReplaceDto
{
    public string SessionName { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string? Chair { get; set; }
    public int Order { get; set; }
    public List<ProgramPresentationReplaceDto> Presentations { get; set; } = [];
}

public class ProgramPresentationReplaceDto
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Authors { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
}
