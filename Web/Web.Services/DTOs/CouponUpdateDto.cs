using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class CouponUpdateDto
{
    public string? Description { get; set; }
    public CouponType Type { get; set; }
    public decimal Value { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int? MaxUses { get; set; }
    public bool Active { get; set; }
}
