using Web.Services.DTOs.InvoiceItem;

namespace Web.Services.Abstractions;

public interface IInvoiceItemService
{
    Task<List<InvoiceItemDto>> GetAllByInvoiceIdAsync(int invoiceDtoId);
    Task<InvoiceItemDto?> GetByIdAsync(int invoiceItemDtoId);
    Task<InvoiceItemDto> AddAsync(CreateInvoiceItemDto createInvoiceItemDto);
}