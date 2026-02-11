using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstractions;
using Web.Services.DTOs.PageContent;

namespace Web.Api.Controllers;

[ApiController]
[Route("api/page-content")]
public class PageContentController : ControllerBase
{
    private readonly IPageContentService _pageContentService;

    public PageContentController(IPageContentService pageContentService)
    {
        _pageContentService = pageContentService;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var pageContents = await _pageContentService.GetAllAsync();
        return Ok(pageContents);
    }

    [HttpGet("get-by-id/{pageContentId:int}", Name = "GetPageContentById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int pageContentId)
    {
        var pageContent = await _pageContentService.GetByIdAsync(pageContentId);
        if (pageContent == null)
        {
            return NotFound($"Page content with id {pageContentId} not found.");
        }

        return Ok(pageContent);
    }

    [HttpGet("page-content-by-nav-bar-menu-id/{navBarMenuId:int}", Name = "GetPageContentByNavBarMenuId")]
    public async Task<IActionResult> GetByNavBarMenuIdAsync([FromRoute] int navBarMenuId)
    {
        var pageContent = await _pageContentService.GetByNavBarMenuIdAsync(navBarMenuId);
        if (pageContent == null)
        {
            return NotFound($"Page content for nav bar menu id {navBarMenuId} not found.");
        }

        return Ok(pageContent);
    }

    [HttpPost("add-page-content")]
    public async Task<IActionResult> AddAsync([FromBody] CreatePageContentDto createPageContentDto)
    {
        var result = await _pageContentService.AddAsync(createPageContentDto);
        return CreatedAtRoute("GetPageContentById", new { pageContentId = result.Id }, result);
    }

    [HttpPut("update-page-content", Name = "UpdatePageContent")]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdatePageContentDto updatePageContentDto)
    {
        var updatedPageContent = await _pageContentService.UpdateAsync(updatePageContentDto);
        if (updatedPageContent == null)
        {
            return NotFound($"Page content with id {updatePageContentDto.Id} not found.");
        }

        return Ok(updatedPageContent);
    }

    [HttpDelete("delete-page-content/{pageContentId:int}", Name = "DeletePageContent")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int pageContentId)
    {
        var isDeleted = await _pageContentService.DeleteAsync(pageContentId);
        if (!isDeleted)
        {
            return NotFound($"Page content with id {pageContentId} not found.");
        }

        return NoContent();
    }
}
