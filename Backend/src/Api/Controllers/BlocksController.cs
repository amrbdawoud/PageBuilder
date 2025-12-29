// src/Api/Controllers/BlocksController.cs
using Microsoft.AspNetCore.Mvc;
using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;

namespace PageBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlocksController : ControllerBase
{
    private readonly IBlockService _blockService;

    public BlocksController(IBlockService blockService)
    {
        _blockService = blockService;
    }

    /// <summary>
    /// Get block by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BlockDTO>> GetById(int id)
    {
        var block = await _blockService.GetByIdAsync(id);
        if (block == null)
            return NotFound(new { message = $"Block with ID {id} not found" });

        return Ok(block);
    }

    /// <summary>
    /// Get all blocks for a page
    /// </summary>
    [HttpGet("page/{pageId}")]
    public async Task<ActionResult<IEnumerable<BlockDTO>>> GetByPageId(int pageId)
    {
        var blocks = await _blockService.GetByPageIdAsync(pageId);
        return Ok(blocks);
    }

    /// <summary>
    /// Create a new block
    /// Block types: PlanCard, PurchaseButton, ConsultationButton, SalesRepCard, Text, Image, etc.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BlockDTO>> Create([FromBody] CreateBlockDTO dto)
    {
        var block = await _blockService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = block.Id }, block);
    }

    /// <summary>
    /// Update a block
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<BlockDTO>> Update(int id, [FromBody] UpdateBlockDTO dto)
    {
        var block = await _blockService.UpdateAsync(id, dto);
        if (block == null)
            return NotFound(new { message = $"Block with ID {id} not found" });

        return Ok(block);
    }

    /// <summary>
    /// Reorder blocks on a page
    /// </summary>
    [HttpPost("page/{pageId}/reorder")]
    public async Task<ActionResult> Reorder(int pageId, [FromBody] ReorderBlocksDTO dto)
    {
        await _blockService.ReorderBlocksAsync(pageId, dto);
        return Ok(new { message = "Blocks reordered successfully" });
    }

    /// <summary>
    /// Delete a block
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _blockService.DeleteAsync(id);
        if (!result)
            return NotFound(new { message = $"Block with ID {id} not found" });

        return NoContent();
    }
}
