using Microsoft.AspNetCore.Mvc;
using PageBuilder.Application.Interfaces;

namespace PageBuilder.Api.Controllers;

/// Public endpoints for viewing published offer pages
[ApiController]
[Route("api/public")]
public class PublishedPagesController : ControllerBase
{
    private readonly IPageService _pageService;

    public PublishedPagesController(IPageService pageService)
    {
        _pageService = pageService;
    }

    /// Get published offer page for a company
    /// This is the public-facing endpoint
    /// URL: /api/public/company/{companyId}/page
    [HttpGet("company/{companyId}/page")]
    public async Task<ActionResult> GetPublishedPage(int companyId)
    {
        var page = await _pageService.GetPublishedByCompanyIdAsync(companyId);
        if (page == null)
            return NotFound(new { message = "No published page found for this company" });

        // TODO: Track page view with sales rep info
        // await _analyticsService.TrackPageViewAsync(page.Id, salesRepId);

        return Ok(page);
    }
}
