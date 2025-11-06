namespace Web.Domain.Models;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Ico { get; set; }
    public string Dic { get; set; }
    public string? IcDph { get; set; }
    public string Bank { get; set; }
    public string Address { get; set; }
    public string AddressPostalCode { get; set; }
    public string AddressCity { get; set; }
    public string BankAccount { get; set; }
    public string Swift { get; set; }
    public string Iban { get; set; }
    
    public string Phone { get; set; }
}