using Web.Services.DTOs.Supplier;

namespace Web.Services.Abstractions;

public interface ISupplierService
{
    Task<SupplierDto?> GetByIdAsync(int id);
}