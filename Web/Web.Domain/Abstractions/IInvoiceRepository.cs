using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IInvoiceRepository
{
    Task<int> GetAllSumAsync(DateOnly date);
    Task<Invoice?> GetByIdAsync(int id);
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
}