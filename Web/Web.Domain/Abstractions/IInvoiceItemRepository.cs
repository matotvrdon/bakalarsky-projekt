using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IInvoiceItemRepository
{
    Task<InvoiceItem?> GetByIdAsync(int invoiceItemId);
    Task AddAsync(InvoiceItem invoiceItem);
}