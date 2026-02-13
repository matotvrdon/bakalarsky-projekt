using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class CateringOrderDto
{
    public int Id { get; set; }
    public int ParticipantId { get; set; }
    public CateringOrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CateringOrderItemDto> Items { get; set; } = new();
}
