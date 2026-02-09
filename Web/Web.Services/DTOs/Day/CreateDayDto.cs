using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Day;

public class CreateDayDto
{
    public DateTime Date { get; set; }

    [Range(1, int.MaxValue)]
    public int ConferenceId { get; set; }
}
