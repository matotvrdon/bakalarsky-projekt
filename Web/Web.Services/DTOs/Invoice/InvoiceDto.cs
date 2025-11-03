using System;

namespace Web.Services.DTOs.Invoice;

public class InvoiceDto
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
}