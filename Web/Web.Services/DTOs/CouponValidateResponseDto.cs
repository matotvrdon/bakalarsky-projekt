namespace Web.Services.DTOs;

public class CouponValidateResponseDto
{
    public bool IsValid { get; set; }
    public string? Message { get; set; }
    public decimal Discount { get; set; }
    public CouponDto? Coupon { get; set; }
}
