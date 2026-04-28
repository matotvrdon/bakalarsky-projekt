using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class InvoiceCreateDto
{
    public int ParticipantId { get; set; }

    public InvoiceType Type { get; set; }

    public InvoiceCustomerType CustomerType { get; set; }

    public string? CustomerName { get; set; }

    public string? CompanyName { get; set; }

    public string BillingAddress { get; set; } = string.Empty;

    public string? Ico { get; set; }

    public string? Dic { get; set; }

    public string? VatId { get; set; }

    public int? BookingOptionId { get; set; }

    public List<int> FoodOptionIds { get; set; } = [];
}