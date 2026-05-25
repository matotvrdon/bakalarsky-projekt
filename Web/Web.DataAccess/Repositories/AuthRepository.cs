using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Abstractions;
using Web.DataAccess.Data;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _dbContext;

    public AuthRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(user => user.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }
}