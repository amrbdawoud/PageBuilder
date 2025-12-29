// src/Api/Controllers/PagesController.cs
using Microsoft.AspNetCore.Mvc;
using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;

namespace PageBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagesController : ControllerBase
{
    private readonly IPageService _pageService;

    public PagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    /// <summary>
    /// Get page by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PageDTO>> GetById(int id)
    {
        var page = await _pageService.GetByIdAsync(id);
        if (page == null)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return Ok(page);
    }

    /// <summary>
    /// Get page with all blocks
    /// </summary>
    [HttpGet("{id}/blocks")]
    public async Task<ActionResult<PageWithBlocksDTO>> GetWithBlocks(int id)
    {
        var page = await _pageService.GetWithBlocksAsync(id);
        if (page == null)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return Ok(page);
    }

    /// <summary>
    /// Get all pages for a company
    /// </summary>
    [HttpGet("company/{companyId}")]
    public async Task<ActionResult<IEnumerable<PageDTO>>> GetByCompanyId(int companyId)
    {
        var pages = await _pageService.GetAllByCompanyIdAsync(companyId);
        return Ok(pages);
    }

    /// <summary>
    /// Create a new page (starts as DRAFT)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<PageDTO>> Create([FromBody] CreatePageDTO dto)
    {
        var page = await _pageService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = page.Id }, page);
    }

    /// <summary>
    /// Update a page
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<PageDTO>> Update(int id, [FromBody] UpdatePageDTO dto)
    {
        var page = await _pageService.UpdateAsync(id, dto);
        if (page == null)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return Ok(page);
    }

    /// <summary>
    /// Publish a page
    /// </summary>
    [HttpPost("{id}/publish")]
    public async Task<ActionResult> Publish(int id)
    {
        var result = await _pageService.PublishAsync(id);
        if (!result)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return Ok(new { message = "Page published successfully" });
    }

    /// <summary>
    /// Unpublish a page (back to draft)
    /// </summary>
    [HttpPost("{id}/unpublish")]
    public async Task<ActionResult> Unpublish(int id)
    {
        var result = await _pageService.UnpublishAsync(id);
        if (!result)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return Ok(new { message = "Page unpublished successfully" });
    }

    /// <summary>
    /// Delete a page (soft delete / archive)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _pageService.DeleteAsync(id);
        if (!result)
            return NotFound(new { message = $"Page with ID {id} not found" });

        return NoContent();
    }
}
