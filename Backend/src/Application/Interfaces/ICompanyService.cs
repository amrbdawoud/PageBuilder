namespace PageBuilder.Application.Interfaces;

using PageBuilder.Application.DTOs;

public interface ICompanyService
{
    Task<CompanyDTO?> GetByIdAsync(int id);
    Task<IEnumerable<CompanyDTO>> GetAllAsync();
    Task<CompanyWithPagesDTO?> GetWithPagesAsync(int id);
    Task<CompanyDTO?> GetByNameAsync(string name);
    Task<CompanyDTO> CreateAsync(CreateCompanyDTO dto);
    Task<CompanyDTO?> UpdateAsync(int id, UpdateCompanyDTO dto);
    Task<bool> DeleteAsync(int id);
}
