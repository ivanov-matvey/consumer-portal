using ConsumerPortal.Api.Contracts.Claims;
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

    [HttpPost]
    [ProducesResponseType(typeof(ClaimDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClaimDto>> Create(
        CreateClaimRequest request,
        CancellationToken cancellationToken
    )
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
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
}
