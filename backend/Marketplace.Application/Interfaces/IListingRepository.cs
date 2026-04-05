using Marketplace.Core.Entities;

namespace Marketplace.Application.Interfaces;

public interface IListingRepository
{
    Task<Listing> AddListingAsync(Listing listing);
    Task<List<Listing>> GetListingsAsync(
        string? keyword = null,
        double? latitude = null,
        double? longitude = null,
        double? radiusKm = null,
        int skip = 0,
        int take = 20);
    Task SaveChangesAsync();
}