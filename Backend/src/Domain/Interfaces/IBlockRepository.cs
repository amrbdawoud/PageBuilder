namespace PageBuilder.Domain.Interfaces;

using PageBuilder.Domain.Entities;

public interface IBlockRepository : IRepository<Block>
{
    Task<IEnumerable<Block>> GetByPageIdAsync(int pageId);
    Task ReorderBlocksAsync(int pageId, List<int> blockIds);
}
