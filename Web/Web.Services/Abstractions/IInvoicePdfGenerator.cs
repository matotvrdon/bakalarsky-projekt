namespace Web.Services.Abstractions;

public interface IInvoicePdfGenerator
{
    Task<byte[]> GeneratePdfAsync(int invoiceId);
}