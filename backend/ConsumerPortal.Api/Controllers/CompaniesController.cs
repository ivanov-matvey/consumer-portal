using ConsumerPortal.Api.Contracts.Claims;
using ConsumerPortal.Api.Contracts.Common;
using ConsumerPortal.Api.Contracts.Companies;
using ConsumerPortal.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsumerPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CompaniesController(ApplicationDbContext dbContext) => _dbContext = dbContext;

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<CompanyDto>>> GetAll(
        string? search,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _dbContext.Companies
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.Trim();
            query = query.Where(company =>
                company.Name.Contains(searchTerm) || company.Inn.Contains(searchTerm)
            );
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var companies = await query
            .OrderBy(company => company.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(company => new CompanyDto(
                company.Id,
                company.Name,
                company.Inn,
                (int)company.Category
            ))
            .ToListAsync(cancellationToken);

        return Ok(new PagedResult<CompanyDto>(companies, page, pageSize, totalCount));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDetailsDto>> GetById(
        Guid id,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var company = await _dbContext.Companies
            .AsNoTracking()
            .Where(company => company.Id == id)
            .Select(company => new
            {
                company.Id,
                company.Name,
                company.Inn,
                Category = (int)company.Category
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (company is null)
        {
            return NotFound();
        }

        var claimsQuery = _dbContext.Claims
            .AsNoTracking()
            .Where(claim => claim.CompanyId == id);
        var totalCount = await claimsQuery.CountAsync(cancellationToken);
        var claims = await claimsQuery
            .OrderByDescending(claim => claim.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(claim => new ClaimDto(
                claim.Id,
                claim.Title,
                claim.Text,
                (int)claim.Status,
                claim.CreatedAt,
                claim.CompanyId,
                claim.UserId
            ))
            .ToListAsync(cancellationToken);

        return Ok(new CompanyDetailsDto(
            company.Id,
            company.Name,
            company.Inn,
            company.Category,
            new PagedResult<ClaimDto>(claims, page, pageSize, totalCount)
        ));
    }
}
