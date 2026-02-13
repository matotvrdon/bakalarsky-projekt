using Web.Domain.Enums;

namespace Web.Domain.Models;

public class Coupon
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public CouponType Type { get; set; }
    public decimal Value { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int? MaxUses { get; set; }
    public int UsedCount { get; set; }
    public bool Active { get; set; }
}