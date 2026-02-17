namespace Web.Domain.Models;

public class Conference
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Location { get; set; }
    public bool IsActive { get; set; }
}