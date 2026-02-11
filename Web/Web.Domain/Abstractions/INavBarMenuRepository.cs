using Web.Domain.Models;

namespace Web.Domain.Abstractions;

public interface INavBarMenuRepository
{
    Task<List<NavBarMenu>> GetNavBarMenus();
    Task<NavBarMenu?> GetNavBarMenu(int id);
    Task AddNavBarMenu(NavBarMenu navBarMenu);
    Task UpdateNavBarMenu(NavBarMenu navBarMenu);
    Task<bool> DeleteNavBarMenu(int id);
}