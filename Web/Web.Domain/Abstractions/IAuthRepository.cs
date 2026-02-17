using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface IAuthRepository
{
    Task<User?> GetByEmailAsync(string email);
}