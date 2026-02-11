using Microsoft.AspNetCore.Mvc;
using Web.Domain.Abstractions;
using Web.Domain.Enums;
using Web.Domain.Models;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/nav-bar-menu")]
public class NavBarMenuController : ControllerBase
{
    private readonly INavBarMenuRepository _navBarMenuRepository;

    public NavBarMenuController(INavBarMenuRepository navBarMenuRepository)
    {
        _navBarMenuRepository = navBarMenuRepository;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var navBarMenus = await _navBarMenuRepository.GetNavBarMenus();
        return Ok(navBarMenus);
    }
    
    [HttpGet("get-all-query")]
    public async Task<IActionResult> GetAllAsync([FromQuery] IsActive? isActive)
    {
        var navBarMenus = await _navBarMenuRepository.GetNavBarMenus();
        if (isActive.HasValue)
        {
            navBarMenus = navBarMenus
                .Where(menu => menu.IsActive == isActive.Value)
                .ToList();
        }
        return Ok(navBarMenus);
    }

    [HttpGet("get-by-id/{navBarMenuId:int}", Name = "GetNavBarMenuById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int navBarMenuId)
    {
        var navBarMenu = await _navBarMenuRepository.GetNavBarMenu(navBarMenuId);

        if (navBarMenu == null)
        {
            return NotFound($"Nav bar menu with id {navBarMenuId} not found.");
        }

        return Ok(navBarMenu);
    }

    [HttpPost("add-nav-bar-menu")]
    public async Task<IActionResult> AddAsync([FromBody] NavBarMenu navBarMenu)
    {
        await _navBarMenuRepository.AddNavBarMenu(navBarMenu);
        return CreatedAtRoute("GetNavBarMenuById", new { navBarMenuId = navBarMenu.Id }, navBarMenu);
    }

    [HttpPut("update-nav-bar-menu", Name = "UpdateNavBarMenu")]
    public async Task<IActionResult> UpdateAsync([FromBody] NavBarMenu navBarMenu)
    {
        var existingNavBarMenu = await _navBarMenuRepository.GetNavBarMenu(navBarMenu.Id);
        if (existingNavBarMenu == null)
        {
            return NotFound($"Nav bar menu with id {navBarMenu.Id} not found.");
        }

        existingNavBarMenu.Name = navBarMenu.Name;
        existingNavBarMenu.IsActive = navBarMenu.IsActive;

        await _navBarMenuRepository.UpdateNavBarMenu(existingNavBarMenu);
        return Ok(existingNavBarMenu);
    }

    [HttpDelete("delete-nav-bar-menu/{navBarMenuId:int}", Name = "DeleteNavBarMenu")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int navBarMenuId)
    {
        var isDeleted = await _navBarMenuRepository.DeleteNavBarMenu(navBarMenuId);
        if (!isDeleted)
        {
            return NotFound($"Nav bar menu with id {navBarMenuId} not found.");
        }

        return NoContent();
    }
}
