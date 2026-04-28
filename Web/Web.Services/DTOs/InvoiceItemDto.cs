using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class InvoiceItemDto
{
    public int Id { get; set; }

    public int? ParticipantId { get; set; }

    public InvoiceItemType Type { get; set; }

    public int? SourceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }
}