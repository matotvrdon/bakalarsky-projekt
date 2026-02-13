using Web.Domain.Enums;

namespace Web.Services.DTOs;

public class RegistrationRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public string? Affiliation { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public RegistrationType RegistrationType { get; set; }
    public bool SubmitPaper { get; set; }
    public string? PaperTitle { get; set; }
    public string? PaperAbstract { get; set; }
    public string? PaperFileUrl { get; set; }
    public bool NeedAccommodation { get; set; }
    public int? AccommodationOptionId { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public List<int> CateringOptionIds { get; set; } = new();
    public string? DietaryRequirements { get; set; }
    public string? SpecialNeeds { get; set; }
    public string? CouponCode { get; set; }
    public string? InvoiceCompany { get; set; }
    public string? InvoiceAddress { get; set; }
    public string? InvoiceIco { get; set; }
    public string? InvoiceDic { get; set; }
}
