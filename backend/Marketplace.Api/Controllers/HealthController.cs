using Marketplace.Application.DTOs;
using Marketplace.Application.Interfaces;
using Marketplace.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly MarketplaceDbContext _context;

    public HealthController(MarketplaceDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    public async Task<IActionResult> Register(RegisterRequestDTO request)
    {
        var canConnect = await _context.Database.CanConnectAsync();

        return Ok();
    }
}