using Microsoft.EntityFrameworkCore;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Interfaces;
using PageBuilder.Infrastructure.Data;

namespace PageBuilder.Infrastructure.Repositories;

public class PageRepository : Repository<Page>, IPageRepository
{
    public PageRepository(AppDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Page>> GetAllByCompanyIdAsync(int companyId)
    {
        return await _dbSet.Where(p => p.CompanyId == companyId).ToListAsync();
    }

    public async Task<Page?> GetByTitleAndCompanyIdAsync(string title, int companyId)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Title == title && p.CompanyId == companyId);
    }

    public async Task<Page?> GetPublishedByCompanyIdAsync(int companyId)
    {
        return await _dbSet.FirstOrDefaultAsync(p =>
            p.CompanyId == companyId && p.PageStatus == Domain.Enums.PageStatus.Published
        );
    }

    public Task<Page?> GetWithBlocksAsync(int id)
    {
        return _dbSet.Include(p => p.Blocks).FirstOrDefaultAsync(p => p.Id == id);
    }
}
