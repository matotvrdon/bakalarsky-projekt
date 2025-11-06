using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface ISupplierRepository
{
    Task<Supplier?> GetByIdAsync(int id);
}