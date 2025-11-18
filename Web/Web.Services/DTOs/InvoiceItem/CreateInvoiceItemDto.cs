namespace Web.Services.DTOs.InvoiceItem;

public class CreateInvoiceItemDto
{
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public int AttendeeId { get; set; }
}