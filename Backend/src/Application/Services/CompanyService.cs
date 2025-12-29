namespace PageBuilder.Application.Services;

using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;
using PageBuilder.Domain.Entities;
using PageBuilder.Domain.Interfaces;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<CompanyDTO?> GetByIdAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return null;

        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            CreatedAt = company.CreatedAt,
        };
    }

    public async Task<IEnumerable<CompanyDTO>> GetAllAsync()
    {
        var companies = await _companyRepository.GetAllAsync();
        return companies.Select(c => new CompanyDTO
        {
            Id = c.Id,
            Name = c.Name,
            CreatedAt = c.CreatedAt,
        });
    }

    public async Task<CompanyWithPagesDTO?> GetWithPagesAsync(int id)
    {
        var companies = await _companyRepository.GetAllWithPagesAsync();
        var company = companies.FirstOrDefault(c => c.Id == id);
        if (company == null)
            return null;

        return new CompanyWithPagesDTO
        {
            Id = company.Id,
            Name = company.Name,
            CreatedAt = company.CreatedAt,
            Pages = company
                .Pages.Select(p => new PageDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    CreatedAt = p.CreatedAt,
                    PageStatus = p.PageStatus,
                    CompanyId = p.CompanyId,
                })
                .ToList(),
        };
    }

    public async Task<CompanyDTO?> GetByNameAsync(string name)
    {
        var company = await _companyRepository.GetByNameAsync(name);
        if (company == null)
            return null;

        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            CreatedAt = company.CreatedAt,
        };
    }

    public async Task<CompanyDTO> CreateAsync(CreateCompanyDTO dto)
    {
        var company = new Company { Name = dto.Name, CreatedAt = DateTime.UtcNow };

        var created = await _companyRepository.AddAsync(company);
        return new CompanyDTO
        {
            Id = created.Id,
            Name = created.Name,
            CreatedAt = created.CreatedAt,
        };
    }

    public async Task<CompanyDTO?> UpdateAsync(int id, UpdateCompanyDTO dto)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return null;

        company.Name = dto.Name;
        await _companyRepository.UpdateAsync(company);

        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            CreatedAt = company.CreatedAt,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return false;

        company.DeletedAt = DateTime.UtcNow;
        await _companyRepository.UpdateAsync(company);
        return true;
    }
}
