using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class AccommodationBookingDto
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public int OptionId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Nights { get; set; }
    public decimal Total { get; set; }
    public AccommodationBookingStatus Status { get; set; }
}
