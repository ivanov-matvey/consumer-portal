using ConsumerPortal.Api.Contracts.Claims;
using ConsumerPortal.Api.Contracts.Common;
using ConsumerPortal.Api.Domain.Entities;
using ConsumerPortal.Api.Domain.Enums;
using ConsumerPortal.Api.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ClaimEntity = ConsumerPortal.Api.Domain.Entities.Claim;

namespace ConsumerPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClaimsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public ClaimsController(ApplicationDbContext dbContext) => _dbContext = dbContext;

    [HttpGet("my")]
    [ProducesResponseType(typeof(PagedResult<UserClaimDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<UserClaimDto>>> GetMy(
        string? search,
        int? status,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _dbContext.Claims
            .AsNoTracking()
            .Where(claim => claim.UserId == userId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.Trim();
            query = query.Where(claim =>
                claim.Title.Contains(searchTerm)
                || claim.Text.Contains(searchTerm)
                || claim.Company.Name.Contains(searchTerm)
            );
        }

        if (status.HasValue)
        {
            query = query.Where(claim => (int)claim.Status == status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var claims = await query
            .OrderByDescending(claim => claim.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(claim => new UserClaimDto(
                claim.Id,
                claim.Title,
                claim.Text,
                (int)claim.Status,
                claim.CreatedAt,
                claim.CompanyId,
                claim.Company.Name
            ))
            .ToListAsync(cancellationToken);

        return Ok(new PagedResult<UserClaimDto>(claims, page, pageSize, totalCount));
    }

    [HttpGet]
    [Authorize(Roles = "Moderator")]
    [ProducesResponseType(typeof(PagedResult<ModerationClaimDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedResult<ModerationClaimDto>>> GetAll(
        string? search,
        int? status,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default
    )
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _dbContext.Claims
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchTerm = search.Trim();
            query = query.Where(claim =>
                claim.Title.Contains(searchTerm)
                || claim.Text.Contains(searchTerm)
                || claim.Company.Name.Contains(searchTerm)
                || claim.User.FullName.Contains(searchTerm)
                || claim.User.Email.Contains(searchTerm)
            );
        }

        if (status.HasValue)
        {
            query = query.Where(claim => (int)claim.Status == status.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var claims = await query
            .OrderByDescending(claim => claim.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(claim => new ModerationClaimDto(
                claim.Id,
                claim.Title,
                claim.Text,
                (int)claim.Status,
                claim.CreatedAt,
                claim.CompanyId,
                claim.Company.Name,
                claim.UserId,
                claim.User.FullName,
                claim.User.Email
            ))
            .ToListAsync(cancellationToken);

        return Ok(new PagedResult<ModerationClaimDto>(claims, page, pageSize, totalCount));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClaimDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClaimDto>> Create(
        CreateClaimRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var company = await _dbContext.Companies
            .AsNoTracking()
            .SingleOrDefaultAsync(company => company.Id == request.CompanyId, cancellationToken);

        if (company is null)
        {
            ModelState.AddModelError(nameof(request.CompanyId), "Выбранная организация не найдена.");
        }
        else if (company.Inn != request.Inn)
        {
            ModelState.AddModelError(nameof(request.Inn), "ИНН не соответствует выбранной организации.");
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var claim = new ClaimEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title.Trim(),
            Text = request.Text.Trim(),
            Status = ClaimStatus.New,
            CreatedAt = DateTimeOffset.UtcNow,
            CompanyId = request.CompanyId,
            UserId = userId
        };

        _dbContext.Claims.Add(claim);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = new ClaimDto(
            claim.Id,
            claim.Title,
            claim.Text,
            (int)claim.Status,
            claim.CreatedAt,
            claim.CompanyId,
            claim.UserId
        );

        return Created($"/api/claims/{claim.Id}", response);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Moderator")]
    [ProducesResponseType(typeof(ClaimDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClaimDto>> UpdateStatus(
        Guid id,
        UpdateClaimStatusRequest request,
        CancellationToken cancellationToken
    )
    {
        var claim = await _dbContext.Claims
            .SingleOrDefaultAsync(item => item.Id == id, cancellationToken);

        if (claim is null)
        {
            return NotFound();
        }

        claim.Status = (ClaimStatus)request.Status;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Ok(new ClaimDto(
            claim.Id,
            claim.Title,
            claim.Text,
            (int)claim.Status,
            claim.CreatedAt,
            claim.CompanyId,
            claim.UserId
        ));
    }

    private bool TryGetUserId(out Guid userId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdClaim, out userId);
    }
}
