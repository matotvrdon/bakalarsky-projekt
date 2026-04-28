using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class InvoiceDto
{
    public int Id { get; set; }

    public int ConferenceId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public InvoiceType Type { get; set; }

    public bool IsShared { get; set; }

    public InvoiceStatus Status { get; set; }

    public string? SharedCode { get; set; }

    public decimal TotalAmount { get; set; }

    public InvoiceCustomerType CustomerType { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string? CompanyName { get; set; }

    public string BillingAddress { get; set; } = string.Empty;

    public string? Ico { get; set; }

    public string? Dic { get; set; }

    public string? VatId { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime DueDateUtc { get; set; }

    public DateTime? PaidAtUtc { get; set; }

    public int? FileManagerId { get; set; }

    public FileManagerDto? FileManager { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = [];

    public List<InvoiceParticipantDto> Participants { get; set; } = [];
}