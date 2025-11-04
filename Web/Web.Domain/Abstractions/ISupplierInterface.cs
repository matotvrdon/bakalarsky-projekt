using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface ISupplierInterface
{
    Task<Supplier?> GetByIdAsync(int id);
}