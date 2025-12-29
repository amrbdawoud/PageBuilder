namespace PageBuilder.Domain.Interfaces;

using PageBuilder.Domain.Entities;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByNameAsync(string name);
    Task<IEnumerable<Company>> GetAllWithPagesAsync();
}
