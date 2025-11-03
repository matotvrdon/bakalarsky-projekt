namespace Web.Services.DTOs.Invoice;

public class CreateInvoiceDto
{
    DateTime IssueDate { get; set; }
    DateTime DueDate { get; set; }
}