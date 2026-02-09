using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Day;

public class UpdateDayDto
{
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
    public DateTime Date { get; set; }
}
