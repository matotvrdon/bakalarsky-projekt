using Web.Domain.Enums;

namespace Web.Domain.Models;

public class FoodOptions
{
    public int Id { get; set; }
    public int ConferenceSettingsId { get; set; }
    public required ConferenceSettings ConferenceSettings { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateOnly Date { get; set; }
    public float Price { get; set; }
    public FoodOptionsType FoodOptionsType { get; set; }
}
