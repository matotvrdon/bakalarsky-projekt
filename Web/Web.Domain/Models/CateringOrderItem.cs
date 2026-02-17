namespace Web.Domain.Models;

public class CateringOrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public CateringOrder? Order { get; set; }
    public int OptionId { get; set; }
    public CateringOption? Option { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}