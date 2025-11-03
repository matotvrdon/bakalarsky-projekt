namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}