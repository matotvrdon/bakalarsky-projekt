namespace Web.Services.DTOs.Supplier;

public class SupplierDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }
    public required string Ico { get; set; }
    public required string Dic { get; set; }
    public string? IcDph { get; set; }
    public required string Bank { get; set; }
    public required string Address { get; set; }
    public required string AddressPostalCode { get; set; }
    public required string AddressCity { get; set; }
    public required string BankAccount { get; set; }
    public required string Swift { get; set; }
    public required string Iban { get; set; }
    public required string Phone { get; set; }
}
