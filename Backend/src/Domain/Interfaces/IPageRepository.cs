namespace PageBuilder.Domain.Interfaces;

using PageBuilder.Domain.Entities;

public interface IPageRepository : IRepository<Page>
{
    Task<Page?> GetByTitleAndCompanyIdAsync(string title, int companyId);
    Task<IEnumerable<Page>> GetAllByCompanyIdAsync(int companyId);

    Task<Page?> GetPublishedByCompanyIdAsync(int companyId);
    Task<Page?> GetWithBlocksAsync(int id);
}
