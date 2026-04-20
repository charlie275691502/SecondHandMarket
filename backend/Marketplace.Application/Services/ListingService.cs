using Marketplace.Application.DTOs;
using Marketplace.Application.Exceptions;
using Marketplace.Application.Interfaces;
using Marketplace.Core.Entities;
using NetTopologySuite.Geometries;

namespace Marketplace.Application.Services;

public class ListingService : IListingService
{
    private readonly IListingRepository _listingRepository;

    public ListingService(
        IListingRepository listingRepository)
    {
        _listingRepository = listingRepository;
    }

    async Task<ListingResponseDTO> IListingService.CreateEmptyListingAsync(Guid userId)
    {
        var listingEntity = await _listingRepository.AddEmptyListingAsync(userId);

        return new ListingResponseDTO(listingEntity);
    }

    async Task<ListingResponseDTO> IListingService.UpdateListingAsync(Guid userId, Guid listingId, UpdateListingRequestDTO request)
    {
        var listingEntity = await _listingRepository.UpdateListingAsync(
            listingId,
            request.Title,
            request.Description,
            request.Price,
            new Point(request.Longitude, request.Latitude),
            request.CoverImageUrl);

        return new ListingResponseDTO(listingEntity);
    }

    async Task<List<ListingResponseDTO>> IListingService.GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20)
    {
        var listings = await _listingRepository.GetListingsAsync(
            keyword,
            latitude,
            longitude,
            radiusKm,
            skip,
            take);

        return listings.Select(listing => new ListingResponseDTO(listing)).ToList();
    }

    async Task<ListingResponseDTO?> IListingService.PublishListingAsync(Guid userId, Guid listingId)
    {
        var listing = await _listingRepository.GetListingByIdAsync(listingId);
        if (listing == null)
        {
            return null;
        }

        listing.Status = Listing.ListingStatus.Published;
        await _listingRepository.SaveChangesAsync();

        return new ListingResponseDTO(listing);
    }
}