using Web.Services.DTOs;

namespace Web.Services.Abstractions;

public interface ISupplierService
{
    Task<SupplierDto?> GetAsync();
}