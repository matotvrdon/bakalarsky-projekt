namespace Web.Services.DTOs;

public class CouponValidateRequestDto
{
    public required string Code { get; set; }
    public decimal Subtotal { get; set; }
}
