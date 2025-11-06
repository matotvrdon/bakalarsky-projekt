using Web.Services.DTOs.Customer;

namespace Web.Services.Abstractions;

public interface ICustomerService
{
    Task<CustomerDto?> GetByIdAsync(int id);
}