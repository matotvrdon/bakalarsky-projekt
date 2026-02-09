using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.Invoice;

public class CreateInvoiceDto
{
    [Range(1, int.MaxValue)]
    public int SupplierId { get; set; }

    [Range(1, int.MaxValue)]
    public int CustomerId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}
