using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class FoodOptionsCreateDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly Date { get; set; }
    public int Price { get; set; }
    public FoodOptionsType FoodOptionsType { get; set; }
}
