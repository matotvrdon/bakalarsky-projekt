using Web.Services.DTOs.Invoice;

namespace Web.Services.Abstractions;

public interface IPdfService
{
    Task<byte[]> GeneratePdf(int invoiceId);
}