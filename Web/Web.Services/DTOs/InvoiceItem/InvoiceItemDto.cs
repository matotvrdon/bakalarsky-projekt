namespace Web.Services.DTOs.InvoiceItem;

public class InvoiceItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; } 
}