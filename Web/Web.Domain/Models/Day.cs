namespace Web.Domain.Models;

public class Day
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    
    public int ConferenceId { get; set; }
    public Conference Conference { get; set; }
    
    public List<Session> Session { get; set; }
}