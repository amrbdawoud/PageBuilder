namespace PageBuilder.Application.Services;

using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Interfaces;

public class BlockService : IBlockService
{
    private readonly IBlockRepository _blockRepository;
    private readonly IPageRepository _pageRepository;

    public BlockService(IBlockRepository blockRepository, IPageRepository pageRepository)
    {
        _blockRepository = blockRepository;
        _pageRepository = pageRepository;
    }

    public async Task<BlockDTO> CreateAsync(CreateBlockDTO dto)
    {
        var block = new Block
        {
            Type = dto.Type,
            Order = dto.Order,
            Content = dto.Content,
            PageId = dto.PageId,
            Page = null!, // Will be loaded by EF
        };

        var created = await _blockRepository.AddAsync(block);
        return new BlockDTO
        {
            Id = created.Id,
            PageId = created.PageId,
            Type = created.Type,
            Content = created.Content,
            Order = created.Order,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var block = await _blockRepository.GetByIdAsync(id);
        if (block == null)
            return false;

        await _blockRepository.DeleteAsync(block);
        return true;
    }

    public async Task<BlockDTO?> GetByIdAsync(int id)
    {
        var block = await _blockRepository.GetByIdAsync(id);
        if (block == null)
            return null;

        return new BlockDTO
        {
            Id = block.Id,
            PageId = block.PageId,
            Type = block.Type,
            Content = block.Content,
            Order = block.Order,
        };
    }

    public async Task<IEnumerable<BlockDTO>> GetByPageIdAsync(int pageId)
    {
        var blocks = await _blockRepository.GetByPageIdAsync(pageId);
        return blocks
            .Select(b => new BlockDTO
            {
                Id = b.Id,
                PageId = b.PageId,
                Type = b.Type,
                Content = b.Content,
                Order = b.Order,
            })
            .ToList();
    }

    public async Task ReorderBlocksAsync(int pageId, ReorderBlocksDTO dto)
    {
        await _blockRepository.ReorderBlocksAsync(pageId, dto.BlockIds);
    }

    public async Task<BlockDTO?> UpdateAsync(int id, UpdateBlockDTO dto)
    {
        var block = await _blockRepository.GetByIdAsync(id);
        if (block == null)
            return null;

        block.Type = dto.Type;
        block.Content = dto.Content;
        await _blockRepository.UpdateAsync(block);
        return new BlockDTO
        {
            Id = block.Id,
            PageId = block.PageId,
            Type = block.Type,
            Content = block.Content,
            Order = block.Order,
        };
    }
}
