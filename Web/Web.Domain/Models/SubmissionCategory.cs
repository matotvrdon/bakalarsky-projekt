namespace Web.Domain.Models;

public class SubmissionCategory
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; }
}