using Microsoft.EntityFrameworkCore;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Interfaces;
using PageBuilder.Infrastructure.Data;

namespace PageBuilder.Infrastructure.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext context)
        : base(context) { }

    public async Task<Company?> GetByNameAsync(string name)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<IEnumerable<Company>> GetAllWithPagesAsync()
    {
        return await _dbSet.Include(c => c.Pages).ToListAsync();
    }
}
