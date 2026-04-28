using Web.Domain.Enums;

namespace Web.Domain.Models;

public class InvoiceItem
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;

    public int? ParticipantId { get; set; }
    public Participant? Participant { get; set; }

    public InvoiceItemType Type { get; set; }

    public int? SourceId { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal TotalPrice { get; set; }
}