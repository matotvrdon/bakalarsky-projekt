using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Invoice
{
    public int Id { get; set; }

    public int ConferenceId { get; set; }
    public Conference Conference { get; set; } = null!;

    public string InvoiceNumber { get; set; } = string.Empty;

    public InvoiceType Type { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

    public string? SharedCode { get; set; }

    public decimal TotalAmount { get; set; }

    public InvoiceCustomerType CustomerType { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string? CompanyName { get; set; }

    public string BillingAddress { get; set; } = string.Empty;

    public string? Ico { get; set; }

    public string? Dic { get; set; }

    public string? VatId { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime DueDateUtc { get; set; }

    public DateTime? PaidAtUtc { get; set; }

    public int? FileManagerId { get; set; }

    public FileManager? FileManager { get; set; }

    public List<InvoiceItem> Items { get; set; } = [];

    public List<InvoiceParticipant> Participants { get; set; } = [];
}