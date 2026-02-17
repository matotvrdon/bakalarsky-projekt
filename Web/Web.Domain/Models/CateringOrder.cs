using Web.Domain.Enums;

namespace Web.Domain.Models;

public class CateringOrder
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public Participant? Participant { get; set; }
    public CateringOrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}