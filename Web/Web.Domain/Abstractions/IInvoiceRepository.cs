using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id);
}