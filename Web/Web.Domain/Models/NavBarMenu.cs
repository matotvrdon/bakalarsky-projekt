using Web.Domain.Enums;

namespace Web.Domain.Models;

public class NavBarMenu
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IsActive IsActive { get; set; } = IsActive.Inactive;
}