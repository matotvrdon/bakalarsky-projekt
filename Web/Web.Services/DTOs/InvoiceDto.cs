using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class InvoiceDto
{
    public int Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public int? ConferenceId { get; set; }
    public InvoiceStatus Status { get; set; }
    public List<int> ParticipantIds { get; set; } = new();
    public List<InvoiceItemDto> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? CouponCode { get; set; }
    public string? SharedCode { get; set; }
    public BillingInfoDto BillingInfo { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
