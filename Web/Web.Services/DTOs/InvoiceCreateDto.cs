namespace Web.Services.DTOs;

public class InvoiceCreateDto
{
    public int? ConferenceId { get; set; }
    public List<int> ParticipantIds { get; set; } = new();
    public List<InvoiceItemDto> Items { get; set; } = new();
    public string? CouponCode { get; set; }
    public BillingInfoDto BillingInfo { get; set; } = new();
}
