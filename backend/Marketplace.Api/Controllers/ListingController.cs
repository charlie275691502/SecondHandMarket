using System.Security.Claims;
using Marketplace.Application.DTOs;
using Marketplace.Application.Exceptions;
using Marketplace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/listings")]
public class ListingController : ControllerBase
{
    private readonly IListingService _listingService;

    public ListingController(IListingService listingService)
    {
        _listingService = listingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateListingRequestDTO request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            throw new UnauthorizedException("Unauthorized");
        }

        var userId = Guid.Parse(userIdClaim);

        await _listingService.CreateListingAsync(userId, request);
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? keyword,
        [FromQuery] double? latitude,
        [FromQuery] double? longitude,
        [FromQuery] double? radiusKm,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20)
    {
        var listing = await _listingService.GetListingsAsync(
            keyword,
            latitude,
            longitude,
            radiusKm,
            skip,
            take);

        return Ok(listing);
    }
}