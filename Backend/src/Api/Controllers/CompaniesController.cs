using Microsoft.AspNetCore.Mvc;
using PageBuilder.Application.DTOs;
using PageBuilder.Application.Interfaces;

namespace PageBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompaniesController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    /// Get all companies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAll()
    {
        var companies = await _companyService.GetAllAsync();
        return Ok(companies);
    }

    /// Get company by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDTO>> GetById(int id)
    {
        var company = await _companyService.GetByIdAsync(id);
        if (company == null)
            return NotFound(new { message = $"Company with ID {id} not found" });

        return Ok(company);
    }

    /// Get company with all its pages
    [HttpGet("{id}/pages")]
    public async Task<ActionResult<CompanyWithPagesDTO>> GetWithPages(int id)
    {
        var company = await _companyService.GetWithPagesAsync(id);
        if (company == null)
            return NotFound(new { message = $"Company with ID {id} not found" });

        return Ok(company);
    }

    /// Create a new company
    [HttpPost]
    public async Task<ActionResult<CompanyDTO>> Create([FromBody] CreateCompanyDTO dto)
    {
        var company = await _companyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
    }

    /// Update an existing company
    [HttpPut("{id}")]
    public async Task<ActionResult<CompanyDTO>> Update(int id, [FromBody] UpdateCompanyDTO dto)
    {
        var company = await _companyService.UpdateAsync(id, dto);
        if (company == null)
            return NotFound(new { message = $"Company with ID {id} not found" });

        return Ok(company);
    }

    /// Delete a company (soft delete)
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _companyService.DeleteAsync(id);
        if (!result)
            return NotFound(new { message = $"Company with ID {id} not found" });

        return NoContent();
    }
}
