using Web.Domain.Models;

namespace Web.DataAccess.Abstractions;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> ExistsAsync(string email);
    Task<User> AddAsync(User user);
}