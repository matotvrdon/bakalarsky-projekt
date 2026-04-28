namespace Web.Services.DTOs;

public class JoinSharedInvoiceDto
{
    public int ParticipantId { get; set; }

    public string SharedCode { get; set; } = string.Empty;

    public int? BookingOptionId { get; set; }

    public List<int> FoodOptionIds { get; set; } = [];
}