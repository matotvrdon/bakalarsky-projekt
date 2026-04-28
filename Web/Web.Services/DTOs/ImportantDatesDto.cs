using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class ImportantDatesDto
{
    public int Id { get; set; }
    public required string Label { get; set; }
    public DateOnly NormalDate { get; set; }
    public DateOnly? UpdatedDate { get; set; }
    public ImportantDatesStatus ImportantDatesStatus { get; set; }
}
