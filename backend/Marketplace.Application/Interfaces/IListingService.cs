using Marketplace.Application.DTOs;

namespace Marketplace.Application.Interfaces;

public interface IListingService
{
    Task<ListingResponseDTO> CreateListingAsync(Guid userId, CreateListingRequestDTO request);
    Task<List<ListingResponseDTO>> GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20);
}