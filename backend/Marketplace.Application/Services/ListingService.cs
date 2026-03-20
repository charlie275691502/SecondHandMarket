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

    async Task<CreateListingResponseDTO> IListingService.CreateListingAsync(Guid userId, CreateListingRequestDTO request)
    {
        var listing = new Listing
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            CreatedAt = DateTime.UtcNow,
            Location = new Point(request.Longitude, request.Latitude),
            UserId = userId
        };

        var listingEntity = await _listingRepository.AddListingAsync(listing);
        await _listingRepository.SaveChangesAsync();

        return new CreateListingResponseDTO
        {
            Id = listingEntity.Id,
            Title = listingEntity.Title,
            Description = listingEntity.Description,
            Price = listingEntity.Price,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            CreatedAt = listingEntity.CreatedAt,
            UserName = listingEntity.User.UserName
        };
    }
}