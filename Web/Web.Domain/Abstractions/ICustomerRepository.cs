using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
}