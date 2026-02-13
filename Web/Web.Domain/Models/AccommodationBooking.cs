using Web.Domain.Enums;

namespace Web.Domain.Models;

public class AccommodationBooking
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public Participant? Participant { get; set; }
    public int OptionId { get; set; }
    public AccommodationOption? Option { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Nights { get; set; }
    public decimal Total { get; set; }
    public AccommodationBookingStatus Status { get; set; }
}