using Marketplace.Core.Entities;
using NetTopologySuite.Geometries;

namespace Marketplace.Application.Interfaces;

public interface IListingRepository
{
    Task<Listing> AddEmptyListingAsync(Guid userId);
    Task<Listing> UpdateListingAsync(
        Guid listingId,
        string title,
        string description,
        decimal price,
        Point location,
        string coverImageUrl);
    Task<List<Listing>> GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20);
    Task<Listing?> GetListingByIdAsync(Guid listingId);
    Task SaveChangesAsync();
}