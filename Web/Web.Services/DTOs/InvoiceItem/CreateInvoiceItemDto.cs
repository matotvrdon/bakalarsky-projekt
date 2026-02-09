using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.InvoiceItem;

public class CreateInvoiceItemDto
{
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Unit { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Quantity { get; set; }

    [Range(1, int.MaxValue)]
    public int AttendeeId { get; set; }
}
