using Web.Services.DTOs.Invoice;

namespace Web.Services.Abstractions;

public interface IInvoiceService
{
    Task<InvoiceDto?> GetByIdAsync(int id);
    Task<InvoiceDto> CreateAsync(CreateInvoiceDto createInvoiceDto);
}