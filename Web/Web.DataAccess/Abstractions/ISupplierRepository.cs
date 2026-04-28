using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface ISupplierRepository
{
    Task<Supplier?> GetAsync();
}