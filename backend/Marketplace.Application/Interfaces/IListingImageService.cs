using Marketplace.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Application.Interfaces;

public interface IListingImageService
{
    Task<ListingImageResponseDTO> CreateListingImageAsync(Guid listingId, IFormFile file);
}