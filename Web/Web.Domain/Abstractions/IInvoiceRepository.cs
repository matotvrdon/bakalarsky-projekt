using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IInvoiceRepository
{
    Task<int> GetAllSumAsync(DateTime date);
    Task<Invoice?> GetByIdAsync(int id);
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
}