using ConsumerPortal.Api.Contracts.Claims;
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
    [ProducesResponseType(typeof(IReadOnlyCollection<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CompanyDto>>> GetAll(CancellationToken cancellationToken)
    {
        var companies = await _dbContext.Companies
            .AsNoTracking()
            .OrderBy(company => company.Name)
            .Select(company => new CompanyDto(
                company.Id,
                company.Name,
                company.Inn,
                (int)company.Category
            ))
            .ToListAsync(cancellationToken);

        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDetailsDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var company = await _dbContext.Companies
            .AsNoTracking()
            .Where(company => company.Id == id)
            .Select(company => new CompanyDetailsDto(
                company.Id,
                company.Name,
                company.Inn,
                (int)company.Category,
                company.Claims
                    .OrderByDescending(claim => claim.CreatedAt)
                    .Select(claim => new ClaimDto(
                        claim.Id,
                        claim.Title,
                        claim.Text,
                        (int)claim.Status,
                        claim.CreatedAt,
                        claim.CompanyId,
                        claim.UserId
                    ))
                    .ToList()
            ))
            .SingleOrDefaultAsync(cancellationToken);

        return company is null ? NotFound() : Ok(company);
    }
}
