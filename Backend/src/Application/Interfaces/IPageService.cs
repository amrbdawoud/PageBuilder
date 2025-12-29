namespace PageBuilder.Application.Interfaces;

using PageBuilder.Application.DTOs;

public interface IPageService
{
    Task<PageDTO?> GetByIdAsync(int id);
    Task<PageWithBlocksDTO?> GetWithBlocksAsync(int id);
    Task<IEnumerable<PageDTO>> GetAllByCompanyIdAsync(int companyId);
    Task<PageWithBlocksDTO?> GetPublishedByCompanyIdAsync(int companyId);
    Task<PageDTO> CreateAsync(CreatePageDTO dto);
    Task<PageDTO?> UpdateAsync(int id, UpdatePageDTO dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> PublishAsync(int id);
    Task<bool> UnpublishAsync(int id);
}
