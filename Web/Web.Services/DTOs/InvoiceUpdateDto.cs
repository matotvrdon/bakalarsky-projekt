using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class InvoiceUpdateDto
{
    public List<InvoiceItemDto> Items { get; set; } = new();
    public InvoiceStatus Status { get; set; }
    public string? CouponCode { get; set; }
    public BillingInfoDto BillingInfo { get; set; } = new();
}
