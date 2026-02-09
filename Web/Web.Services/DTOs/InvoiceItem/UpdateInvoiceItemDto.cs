using System.ComponentModel.DataAnnotations;

namespace Web.Services.DTOs.InvoiceItem;

public class UpdateInvoiceItemDto
{
    [Range(1, int.MaxValue)]
    public int AttendeeId { get; set; }

    [Range(1, int.MaxValue)]
    public int InvoiceItemId { get; set; }
}
