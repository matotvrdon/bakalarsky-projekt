namespace Web.Services.DTOs;

public class ImportantDatesUpdateDto
{
    public required string Label { get; set; }
    public DateOnly NormalDate { get; set; }
}
