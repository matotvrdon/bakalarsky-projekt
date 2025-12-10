namespace Web.Domain.Models;

public class Conference
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public List<Day> Day { get; set; }
}