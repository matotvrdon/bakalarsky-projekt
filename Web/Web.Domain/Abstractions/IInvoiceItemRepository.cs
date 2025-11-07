using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IInvoiceItemRepository
{
    Task<List<InvoiceItem>> GetAllByInvoiceIdAsync(int invoiceId);
    Task<InvoiceItem?> GetByIdAsync(int invoiceItemId);
    Task AddAsync(InvoiceItem invoiceItem);
}