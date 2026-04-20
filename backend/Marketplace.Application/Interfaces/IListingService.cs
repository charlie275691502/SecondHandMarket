using Marketplace.Application.DTOs;
using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IListingService
{
    Task<ListingResponseDTO> CreateEmptyListingAsync(Guid userId);
    Task<ListingResponseDTO> UpdateListingAsync(Guid userId, Guid listingId, UpdateListingRequestDTO request);
    Task<List<ListingResponseDTO>> GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20);
    Task<ListingResponseDTO?> PublishListingAsync(Guid userId, Guid listingId);
}