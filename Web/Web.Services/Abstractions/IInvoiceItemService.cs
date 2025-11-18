using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Abstractions;

public interface IInvoiceItemService
{
    Task<InvoiceItemDto?> GetByIdAsync(int invoiceItemId);
    Task<InvoiceItemDto> AddAsync(CreateInvoiceItemDto createInvoiceItemDto);
}