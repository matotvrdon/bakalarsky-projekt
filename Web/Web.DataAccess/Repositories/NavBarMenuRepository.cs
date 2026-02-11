using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.Domain.Abstractions;
using Web.Domain.Models;

namespace Web.DataAccess.Repositories;

public class NavBarMenuRepository : INavBarMenuRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NavBarMenuRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<NavBarMenu>> GetNavBarMenus()
    {
        return await _dbContext.NavBarMenu
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<NavBarMenu?> GetNavBarMenu(int id)
    {
        return await _dbContext.NavBarMenu
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task AddNavBarMenu(NavBarMenu navBarMenu)
    {
        await _dbContext.AddAsync(navBarMenu);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateNavBarMenu(NavBarMenu navBarMenu)
    {
        _dbContext.Update(navBarMenu);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteNavBarMenu(int id)
    {
        var entity = await _dbContext.NavBarMenu.FirstOrDefaultAsync(i => i.Id == id);

        if (entity == null)
        {
            return false;
        }
        
        _dbContext.NavBarMenu.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
        
    }
}