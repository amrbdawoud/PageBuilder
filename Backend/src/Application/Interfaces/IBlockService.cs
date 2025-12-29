namespace PageBuilder.Application.Interfaces;

using PageBuilder.Application.DTOs;

public interface IBlockService
{
    Task<BlockDTO?> GetByIdAsync(int id);
    Task<IEnumerable<BlockDTO>> GetByPageIdAsync(int pageId);
    Task<BlockDTO> CreateAsync(CreateBlockDTO dto);
    Task<BlockDTO?> UpdateAsync(int id, UpdateBlockDTO dto);
    Task<bool> DeleteAsync(int id);
    Task ReorderBlocksAsync(int pageId, ReorderBlocksDTO dto);
}
