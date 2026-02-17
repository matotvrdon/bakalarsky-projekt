namespace Web.Services.DTOs;

public class SubmissionCategoryDto
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; }
}
