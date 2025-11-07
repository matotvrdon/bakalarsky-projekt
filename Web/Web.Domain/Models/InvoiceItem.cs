using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Domain.Models;

public class InvoiceItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}