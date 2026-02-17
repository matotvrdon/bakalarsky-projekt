using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public int? ConferenceId { get; set; }
    public Conference? Conference { get; set; }
    public InvoiceStatus Status { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public string? SharedCode { get; set; }
    public string? BillingCompany { get; set; }
    public string? BillingIco { get; set; }
    public string? BillingDic { get; set; }
    public string? BillingAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}