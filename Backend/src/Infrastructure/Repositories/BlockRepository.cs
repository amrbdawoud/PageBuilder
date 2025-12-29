using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Interfaces;
using PageBuilder.Infrastructure.Data;

namespace PageBuilder.Infrastructure.Repositories;

public class BlockRepository : Repository<Block>, IBlockRepository
{
    public BlockRepository(AppDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Block>> GetByPageIdAsync(int pageId)
    {
        return await _dbSet.Where(b => b.PageId == pageId).OrderBy(b => b.Order).ToListAsync();
    }

    public async Task ReorderBlocksAsync(int pageId, List<int> blockIds)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var blocks = await _dbSet.Where(b => b.PageId == pageId).ToListAsync();

            for (int i = 0; i < blockIds.Count; i++)
            {
                var block = blocks.FirstOrDefault(b => b.Id == blockIds[i]);
                if (block != null)
                {
                    block.Order = i;
                }
            }
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAndReorderAsync(int blockId)
    {
        var block = await _dbSet.FindAsync(blockId);
        if (block == null)
            return;

        var pageId = block.PageId;
        _dbSet.Remove(block);

        // Reorder remaining blocks
        var remainingBlocks = await _dbSet
            .Where(b => b.PageId == pageId && b.Order > block.Order)
            .ToListAsync();

        foreach (var b in remainingBlocks)
            b.Order--;

        await _context.SaveChangesAsync();
    }
}
