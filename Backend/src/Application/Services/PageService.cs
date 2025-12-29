namespace PageBuilder.Application.Services;

using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Enums;
using PageBuilder.Domain.Interfaces;

public class PageService : IPageService
{
    private readonly IPageRepository _pageRepository;

    public PageService(IPageRepository pageRepository)
    {
        _pageRepository = pageRepository;
    }

    public async Task<PageDTO> CreateAsync(CreatePageDTO pageDTO)
    {
        var page = new Page
        {
            Title = pageDTO.Title,
            CompanyId = pageDTO.CompanyId,
            PageStatus = PageStatus.Draft,
            CreatedAt = DateTime.UtcNow,
            Company = null!, // To be set by ORM
        };

        var created = await _pageRepository.AddAsync(page);
        return new PageDTO
        {
            Id = created.Id,
            Title = created.Title,
            CreatedAt = created.CreatedAt,
            PageStatus = created.PageStatus,
            CompanyId = created.CompanyId,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
            return false;

        page.PageStatus = PageStatus.Draft;
        page.DeletedAt = DateTime.UtcNow;
        await _pageRepository.UpdateAsync(page);
        return true;
    }

    public async Task<IEnumerable<PageDTO>> GetAllByCompanyIdAsync(int companyId)
    {
        var pages = await _pageRepository.GetAllByCompanyIdAsync(companyId);
        return pages.Select(p => new PageDTO
        {
            Id = p.Id,
            Title = p.Title,
            CreatedAt = p.CreatedAt,
            PageStatus = p.PageStatus,
            CompanyId = p.CompanyId,
        });
    }

    public async Task<PageDTO?> GetByIdAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
            return null;

        return new PageDTO
        {
            Id = page.Id,
            Title = page.Title,
            CreatedAt = page.CreatedAt,
            PageStatus = page.PageStatus,
            CompanyId = page.CompanyId,
        };
    }

    public async Task<PageWithBlocksDTO?> GetPublishedByCompanyIdAsync(int companyId)
    {
        var page = await _pageRepository.GetPublishedByCompanyIdAsync(companyId);
        if (page == null)
            return null;

        return new PageWithBlocksDTO
        {
            Id = page.Id,
            Title = page.Title,
            CreatedAt = page.CreatedAt,
            PageStatus = page.PageStatus,
            CompanyId = page.CompanyId,
            Blocks = page
                .Blocks.Select(b => new BlockDTO
                {
                    Id = b.Id,
                    Type = b.Type,
                    Order = b.Order,
                    Content = b.Content,
                    PageId = b.PageId,
                })
                .ToList(),
        };
    }

    public async Task<PageWithBlocksDTO?> GetWithBlocksAsync(int id)
    {
        var page = await _pageRepository.GetWithBlocksAsync(id);
        if (page == null)
            return null;

        return new PageWithBlocksDTO
        {
            Id = page.Id,
            Title = page.Title,
            CreatedAt = page.CreatedAt,
            PageStatus = page.PageStatus,
            CompanyId = page.CompanyId,
            Blocks = page
                .Blocks.Select(b => new BlockDTO
                {
                    Id = b.Id,
                    Type = b.Type,
                    Order = b.Order,
                    Content = b.Content,
                    PageId = b.PageId,
                })
                .ToList(),
        };
    }

    public async Task<bool> PublishAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
            return false;

        page.PageStatus = PageStatus.Published;
        await _pageRepository.UpdateAsync(page);
        return true;
    }

    public async Task<bool> UnpublishAsync(int id)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
            return false;

        page.PageStatus = PageStatus.Draft;
        await _pageRepository.UpdateAsync(page);
        return true;
    }

    public async Task<PageDTO?> UpdateAsync(int id, UpdatePageDTO dto)
    {
        var page = await _pageRepository.GetByIdAsync(id);
        if (page == null)
            return null;

        page.Title = dto.Title;
        page.PageStatus = dto.PageStatus;
        await _pageRepository.UpdateAsync(page);
        return new PageDTO
        {
            Id = page.Id,
            Title = page.Title,
            CreatedAt = page.CreatedAt,
            PageStatus = page.PageStatus,
            CompanyId = page.CompanyId,
        };
    }
}
